
namespace eVote360.Core.Domain.Entities.Election
{
    public class ElectionResult
    {
        public required string PositionName { get; set; } 
        public required string CandidateNamer { get; set; } 
        public int TotalVotes { get; set; }
        public double Percentage { get; set; }
        public required string PartyName { get; set; } 
        public required string PartyAcronym { get; set; }
        public required string PartyLogo { get; set; } 
    }
}
