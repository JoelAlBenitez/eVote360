
namespace eVote360.Core.Application.DTOs.Elector.Dashboard
{
    public sealed record  WindowsElectivePositionsDto
    {
        public required  int IdElectivePosition {  get; set; }
        public required string NameElectivePosition { get; set; }
        public required int NumberPoliticalParty { get; set; }
        public required int NumberActualCandidactes { get; set; }
        public required bool IsSelection { get; set; }
    }
}
