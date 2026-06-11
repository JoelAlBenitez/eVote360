using System.Text.RegularExpressions;

namespace eVote360.Core.Domain.Settings.ValueObjects.Identifications
{
    public sealed record  IdentificationN
    {
        public string Value { get;}
        private IdentificationN() { }

        public IdentificationN(string value)
        {
            if(string.IsNullOrEmpty(value.Trim())) 
                throw new ArgumentNullException("Identificación ingresada no valida.");

            var valueValid = Regex.Replace(value, "/[-.\\s]/g", "").Trim();
            if (valueValid.Length > 11)
                throw new ArgumentException("Identificación ingresada con longitud no valida.");

            Value = valueValid;
        }
    }
}
