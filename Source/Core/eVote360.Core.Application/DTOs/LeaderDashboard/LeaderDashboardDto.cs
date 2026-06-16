namespace eVote360.Core.Application.DTOs.LeaderDashboard
{
    public record LeaderDashboardDto
    {
        public string PartyName { get; init; } = string.Empty;
        public string PartyAcronym { get; init; } = string.Empty;
        public string? PartyLogoUrl { get; init; }

        public int ActiveCandidatesCount { get; init; }
        public int InactiveCandidatesCount { get; init; }
        public int ApprovedAlliancesCount { get; init; }
        public int PendingAllianceRequestsCount { get; init; }
        public int AssignedCandidatesToPositionsCount { get; init; }
    }
}