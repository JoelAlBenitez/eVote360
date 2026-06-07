using eVote360.Core.Domain.Common.CodeErrors;
using System;

namespace eVote360.Core.Domain.Entities.Candidate.ValueObjects
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

            Name = name;
            LastName = lastName;
        }
    }
}
