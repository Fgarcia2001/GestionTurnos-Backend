using System.ComponentModel.DataAnnotations;

namespace GestionTurnos.Application.Request
{
    public class BusinessUpdateRequest
    {
        
     
        public required string Name { get; set; } = string.Empty;

       
        
        public required string Phone { get; set; } = string.Empty;

       
        public required string LogoUrl { get; set; } = string.Empty;
    }
}