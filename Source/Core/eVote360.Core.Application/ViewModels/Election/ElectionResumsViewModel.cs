

namespace eVote360.Core.Application.ViewModels.Election
{
    public class ElectionResumsViewModel
    {
        public required string PositionName { get; set; }
        public required string CandidateName { get; set; }
        public required string PartyName { get; set; }
        public required string PartyAcronym { get; set; }
        public required string PartyLogo { get; set; }
        public int TotalVotes { get; set; }
        public double Percentage { get; set; }
        public required string ResultStatus { get; set; }
    }
}
