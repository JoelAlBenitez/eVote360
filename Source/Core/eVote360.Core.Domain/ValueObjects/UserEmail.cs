using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.ValueObjects
{
    public record UserEmail
    {
        public string Value { get; }

        public UserEmail(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El email es requerido");
            
            value = value.Trim();

            string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(value, emailRegex))
                throw new ArgumentException("Debe ingresar un correo electrónico con formato válido.");

            Value = value.ToLower();
        }
    }
}
