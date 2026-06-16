namespace eVote360.Core.Application.ViewModels.Elector.Dashboard
{
    public sealed class ElectorElectionPositionViewModel
    {
        public required string NameElectivePosition { get; set; }
        public required string IdElectivePosition { get; set; }
        public required int NumberPoliticalParty { get; set; }
        public required int NumberOfActualCandidactes { get; set; }
        public required bool StateSelection { get; set; }
    }
}
