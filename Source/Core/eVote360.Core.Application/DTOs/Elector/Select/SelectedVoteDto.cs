namespace eVote360.Core.Application.DTOs.Elector.Select
{
    public sealed record class SelectedVoteDto
    {
        public required int IdElectivePosition { get; set; }
        public int? IdCandidate { get; set; }
        public required bool IsNoApply { get; set; }
    }
}
