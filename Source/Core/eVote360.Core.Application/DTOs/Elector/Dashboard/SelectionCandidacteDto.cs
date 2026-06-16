namespace eVote360.Core.Application.DTOs.Elector.Dashboard
{
    public sealed record  SelectionCandidacteDto
    {
        public required int IdCandidacte {  get; set; }
        public required string PhotoCandidacte { get; set; }
        public required string NameCandidacte { get; set; }
        public required string PoliticalParty {  get; set; }
        public required string PoliticalPartyLogoUrl { get; set; }
    }
}
