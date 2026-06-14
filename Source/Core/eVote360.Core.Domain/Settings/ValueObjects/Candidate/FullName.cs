using eVote360.Core.Domain.Common.CodeErrors;
using System.Text.RegularExpressions;

namespace eVote360.Core.Domain.Settings.ValueObjects.Candidate
{
    public sealed record FullName
    {
        public string Name { get;  init; }
        public string LastName { get;  init; }

        private FullName() { }

        public FullName(string name, string lastName)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(CandidatesError.NameInvalid.Description);

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException(CandidatesError.LastNameInvalid.Description);

            if (name.Length > 50)
                throw new ArgumentException(CandidatesError.NameInvalid.Description);

            if (!Regex.IsMatch(name, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                throw new ArgumentException(CandidatesError.NameInvalid.Description);

            if (lastName.Length > 50)
                throw new ArgumentException(CandidatesError.LastNameInvalid.Description);

            if (!Regex.IsMatch(lastName, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                throw new ArgumentException(CandidatesError.LastNameInvalid.Description);
            Name = name;
            LastName = lastName;
        }
    }
}
