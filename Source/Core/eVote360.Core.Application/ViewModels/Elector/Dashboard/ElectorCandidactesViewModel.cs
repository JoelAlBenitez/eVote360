
namespace eVote360.Core.Application.ViewModels.Elector.Dashboard
{
    public sealed class ElectorCandidactesViewModel
    {
        public required int IdCandidacte {  get; set; }
        public required string PhotoUrlCandidacte { get; set; }
        public required string NameCandidacte { get; set; }
        public required string PoliticalParty {  get; set; }
        public required string LogoPoliticalParty { get; set; }
        public required int IdPoliticalParty { get; set;}
    }
}
