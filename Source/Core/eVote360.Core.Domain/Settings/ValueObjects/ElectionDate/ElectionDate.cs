using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Settings.ValueObjects.ElectionDate
{
    public record ElectionDate
    {
        public DateTime Value { get; }

        public ElectionDate(DateTime value)
        {
            Value = value;
        }

        public override string ToString() => Value.ToString("yyyy-MM-dd");
    }
}
