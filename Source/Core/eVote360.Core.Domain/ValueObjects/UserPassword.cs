using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.ValueObjects
{
    public record UserPassword
    {
        public string HashValue { get; }  

        public UserPassword(string hashValue)
        {
            if (string.IsNullOrWhiteSpace(hashValue))
                throw new ArgumentException("La contraseña es requerida");

            HashValue = hashValue;
        }


        public static void ValidateComplexity(string plainPassword)
        {
            if (string.IsNullOrWhiteSpace(plainPassword))
                throw new ArgumentException("La contraseña es requerida");

            if (plainPassword.Length < 8)
                throw new ArgumentException("La contraseña debe tener al menos 8 caracteres");

           if (!Regex.IsMatch(plainPassword, @"[a-zA-Z]") || !Regex.IsMatch(plainPassword, @"[0-9]"))
            throw new ArgumentException("La contraseña debe contener al menos una letra y un número");
                
        }
    } 
}
