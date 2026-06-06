using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.ValueObjects
{
    public record ElectionName
    {
        public string Value { get; }

        public ElectionName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El nombre de la eleccion es requerido", nameof(value));
            }
            Value = value;

            if (value.Length > 100)
            {
                throw new ArgumentException("El nombre de la eleccion no puede tener mas de 100 caracteres", nameof(value));
            }
             Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
