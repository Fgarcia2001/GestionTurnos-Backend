using GestionTurnos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestionTurnos.Aplication.Request
{
    public class BusinessRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]

        public string Password { get; set; } = string.Empty;

        [Url]
        public string? LinkPhoto { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        [Required]

        public Rol Rol { get; set; }

        [Required]
        public string BusinessCategory { get; set; } = string.Empty; 

    }
}
