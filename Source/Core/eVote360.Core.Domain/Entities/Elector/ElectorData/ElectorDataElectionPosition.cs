namespace eVote360.Core.Domain.Entities.Elector.ElectorData
{
    public sealed class ElectorDataElectionPosition
    {

        public required int IdElectivePosition { get; set; }
        public required string NameElectivePosiction { get; set; }
        public required int NumberPoliticalPartyParticiped { get; set; }
        public required int NumberActualCandidacte { get; set; }
    }
}
