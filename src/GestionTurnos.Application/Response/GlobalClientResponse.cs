namespace GestionTurnos.Application.Response
{
    public class GlobalClientResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        // Datos extra relacionales visibles solo por el SysAdmin
        public Guid BusinessId { get; set; }
        public string BusinessName { get; set; } = string.Empty;
    }
}