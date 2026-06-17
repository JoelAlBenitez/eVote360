using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Settings.ValueObjects.PoliticalPartyAcronym
{
    public  class PoliticalPartyAcronym
    {
        public string Value { get; }

        public PoliticalPartyAcronym(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El acrónimo del partido político no puede estar vacío.");
            Value = value.Length > 3 ? value.Substring(0, 3).ToUpper() : value.ToUpper();
        }
    }
}
