using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.ValueObjects
{
    public record ElectionDate
    {
        public DateTime Value { get; }

        public ElectionDate(DateTime value)
        {
            if (value < DateTime.Now)
            {
                throw new ArgumentException("La fecha de la eleccion no puede ser una fecha pasada");
            }
            Value = value;
        }

        public override string ToString() => Value.ToString("yyyy-MM-dd");
    }
}
