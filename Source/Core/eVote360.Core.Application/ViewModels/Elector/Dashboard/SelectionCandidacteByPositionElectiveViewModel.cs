namespace eVote360.Core.Application.ViewModels.Elector.Dashboard
{
    public sealed class SelectionCandidacteByPositionElectiveViewModel
    {
        public required int IdPosictionElective { get; set; }
        public string? PositionName { get; set; }
        public int? IdCandidacteSelection { get; set; }
        public string? NameCandidacte { get; set; }
        public string? PhotoUrlCandidacte { get; set; }
        public string? PoliticalParty { get; set; }
        public string? LogoPoliticalParty { get; set; }
        public int? NumberPoliticalParty { get; set; }
        public required bool NoApplyCandidacte { get; set; }

        public bool NotSelectionCandidate => NoApplyCandidacte;
    }
}
