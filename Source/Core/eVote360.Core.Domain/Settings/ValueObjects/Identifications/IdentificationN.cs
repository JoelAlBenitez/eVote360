using System.Text.RegularExpressions;

namespace eVote360.Core.Domain.Settings.ValueObjects.Identifications
{
    public sealed record  IdentificationN
    {
        public string Value { get;}
        private IdentificationN() { Value = null!; }

        public IdentificationN(string value)
        {
            if(string.IsNullOrEmpty(value)) 
                throw new ArgumentNullException("Identificación ingresada no valida.");

            var valueValid = Regex.Replace(value, "[-.\\s]", "").Trim();
            if (valueValid.Length > 11)
                throw new ArgumentException("Identificación ingresada con longitud no valida.");

            Value = valueValid;
        }
    }
}
