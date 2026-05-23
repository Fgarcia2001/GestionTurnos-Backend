using GestionTurnos.Application.Abstraction.Infrastructure;
using GestionTurnos.Application.Abstraction.Infrastructure.Auth;
using GestionTurnos.Application.Exceptions;
using GestionTurnos.Application.Mapper;
using GestionTurnos.Application.Request;
using GestionTurnos.Application.Response;
using GestionTurnos.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestionTurnos.Infrastructure.ExternalServices
{
    public class AuthService : IAuthService
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IStaffRepository staffRepository, IConfiguration configuration)
        {
            _staffRepository = staffRepository;
            _configuration = configuration;
        }

        // Registro de un nuevo negocio, nueva sucursal, se agrega el plan y su staff asociado (Admin) con devolución de token JWT
        public AuthResponse? SignUp(SignUpRequest request)
        {
            bool emailExists = _staffRepository.GetAll().Any(s => s.Email == request.Email);
            if (emailExists)
            {
                throw new ConflictException("El correo electrónico ya está registrado.");
            }

            
            if (!Enum.TryParse<TypeBusiness>(request.BusinessCategory, ignoreCase: true, out var typeBusinessParsed))
            {
                //throw new BadRequestException($"La categoría de negocio '{request.BusinessCategory}' no es válida.");
            }

            var newBusiness = new Business
            {
                Id = Guid.NewGuid(),
                Name = $"{request.Name} - {request.BusinessCategory}",
                Url = $"http://www.{request.Name.Replace(" ", "")}.FCMTurniFy.com",
                TypeBusiness = typeBusinessParsed
            };

            var newBranch = new Branch
            {
                Id = Guid.NewGuid(),
                Address = request.Address,
                Phone = request.BranchPhone,
                BusinessId = newBusiness.Id,
                City = request.City,
                Business = newBusiness
            };

            // El rol de Admin se fuerza de manera interna y segura dentro del Mapper
            var newStaff = request.ToRegisterNewBusinessAndStaff(newBusiness, newBranch);
            _staffRepository.Add(newStaff);

            return new AuthResponse
            {
                // Pasamos el ID del Staff y explícitamente el ID del negocio recién creado
                Token = GenerarToken(newStaff.Id, newStaff.Name, newStaff.Rol, newBusiness.Id),
            };
        }

        public AuthResponse? SignIn(SignInRequest request)
        {
            var user = _staffRepository.GetAll().FirstOrDefault(s => s.Email == request.Email);
            if (user == null)
            {
                throw new Exception("Credenciales Incorrectas."); // O una excepción personalizada propia
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return null;
            }

            return new AuthResponse
            {
                
                Token = GenerarToken(user.Id, user.Name, user.Rol, user.BusinessId),
            };
        }

        
        private string GenerarToken(Guid userId, string nameStaff, Rol rol, Guid? businessId)
        {
            string key = _configuration["Jwt:Key"]!;
            string issuer = _configuration["Jwt:Issuer"]!;
            string audience = _configuration["Jwt:Audience"]!;
            int expirationMinutes = int.Parse(_configuration["Jwt:ExpirationMinutes"]!);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()), 
                new Claim(JwtRegisteredClaimNames.Name, nameStaff),
                new Claim(ClaimTypes.Role, rol.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            // 2. DETALLE CLAVE: El claim BusinessId solo se inyecta si existe un ID de negocio asociado.
            // Si el rol es Sysadmin, businessId llegará como null y se omitirá de forma limpia y segura.
            if (businessId.HasValue && businessId.Value != Guid.Empty)
            {
                claims.Add(new Claim("BusinessId", businessId.Value.ToString()));
            }

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}