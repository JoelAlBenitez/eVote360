namespace eVote360.Core.Application.ViewModels.Elector.Dashboard
{
    public sealed class SelectionCandidacteByPositionElectiveViewModel
    {
        public required int IdPosictionElective { get; set; }
        public  int?  IdCandidacteSelection {  get; set; }
        public required bool NoApplyCandidacte { get; set; }
    }
}
