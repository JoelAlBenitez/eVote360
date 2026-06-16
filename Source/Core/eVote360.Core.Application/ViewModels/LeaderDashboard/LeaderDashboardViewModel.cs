namespace eVote360.Core.Application.ViewModels.LeaderDashboard
{
    public sealed class LeaderDashboardViewModel
    {
        public string PartyName { get; set; } = string.Empty;
        public string PartyAcronym { get; set; } = string.Empty;
        public string? PartyLogoUrl { get; set; }

        public int ActiveCandidatesCount { get; set; }
        public int InactiveCandidatesCount { get; set; }
        public int ApprovedAlliancesCount { get; set; }
        public int PendingAllianceRequestsCount { get; set; }
        public int AssignedCandidatesToPositionsCount { get; set; }
    }
}
