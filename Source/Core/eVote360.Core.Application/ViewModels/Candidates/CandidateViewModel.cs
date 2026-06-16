using eVote360.Core.Application.ViewModels.Base;

namespace eVote360.Core.Application.ViewModels.Candidates
{
    public sealed class CandidateViewModel : ViewModelBase<int>
    {
        public required string LastName { get; set; }
        public string? PhotoUrl { get; set; }
        public int PoliticalPartyId { get; set; }
        public bool HasParticipatedInElection { get; set; }
    }
}
