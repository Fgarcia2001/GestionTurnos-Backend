using GestionTurnos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestionTurnos.Aplication.Response
{
    public class UserResponse
    {
        public String UserName { get; set; } = string.Empty;

        [EmailAddress]
        public String Email { get; set; }  = string.Empty;

        public string LinkPhoto { get; set; } = string.Empty;

        [Phone]
        public double Phone { get; set; }

        public string BusinessName {  get; set; } = String.Empty;
    }
}
