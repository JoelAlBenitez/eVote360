namespace eVote360.Core.Application.DTOs.Admin
{
    public class AdminDto
    {
        public required string NameElection { get; set; }
        public required DateTime DateRealized { get; set; }
        public required int NumberParticipatingMatches { get; set; }
        public required int NumberCandidactesParticipating { get; set; }
        public required int NumberCitizenParticipating { get; set; }
    }
}
