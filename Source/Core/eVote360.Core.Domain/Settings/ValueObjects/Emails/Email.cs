using System.Text.RegularExpressions;

namespace eVote360.Core.Domain.Settings.ValueObjects.Emails
{
    public sealed record  Email
    {
        public string Value { get; }
        private Email() { Value = null!; }
        public Email(string value) { 
        
            if(string.IsNullOrEmpty(value.Trim()) || value.Length > 254 ) 
                throw new ArgumentNullException("Error : Email no valido ingresado.");
            if (!Regex.IsMatch(value.Trim(), @"^[\w\-\.]+@([\w\-]+\.)+[\w\-]{2,4}$"))
                throw new ArgumentException("Error: Email ingresado no tiene un formato valido.");
            Value = value;
        }
    }
}
