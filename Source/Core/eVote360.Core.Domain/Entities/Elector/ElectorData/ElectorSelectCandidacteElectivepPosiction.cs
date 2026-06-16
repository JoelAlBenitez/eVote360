namespace eVote360.Core.Domain.Entities.Elector.ElectorData
{
    public sealed class ElectorSelectCandidacteElectivepPosiction
    {
        public required int IdCandidate { get; set; }
        public required string NameCandidacte {  get; set; }
        public required string PhotoUrlOfCandidacte { get; set; }
        public required string PoliticalParty { get; set; }
        public required string LogoPoliticalParty { get; set; }
    }
}
