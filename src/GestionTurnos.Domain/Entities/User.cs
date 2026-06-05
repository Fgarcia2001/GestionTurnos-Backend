using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestionTurnos.Domain.Entities
{
    public class User : BaseEntity
    {
        public required string Name { get; set; } = string.Empty;

        [EmailAddress]
        public required string Email { get; set; } = string.Empty;
        [Phone]
        public required string Phone { get; set; } = string.Empty;

        private string _password = string.Empty;

        public string Password
        {
            get => _password; set => _password = BCrypt.Net.BCrypt.HashPassword(value);
        }
    }
}
