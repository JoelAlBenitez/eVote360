namespace eVote360.Core.Application.DTOs.PoliticalLeaderAssignment
{
    public class LeaderAssignmentDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string LeaderFullName { get; set; } = string.Empty;
        public string LeaderUsername { get; set; } = string.Empty;
        public int PoliticalPartyId { get; set; }
        public string PartyName { get; set; } = string.Empty;
        public string PartySiglas { get; set; } = string.Empty;
        public bool LeaderState { get; set; }
        public bool PartyState { get; set; }
    }
}
