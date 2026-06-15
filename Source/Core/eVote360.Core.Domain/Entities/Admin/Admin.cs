namespace eVote360.Core.Domain.Entities.Admin
{
    public sealed class Admin
    {
        public required string NameElection {get; set;}
        public required DateTime DateRealized {get; set;}
        public required int NumberParticipatingMatches {get; set;}
        public required int NumberCandidactesParticipating { get; set;}
        public required int NumberCitizenParticipating {get; set;}
    }
}
