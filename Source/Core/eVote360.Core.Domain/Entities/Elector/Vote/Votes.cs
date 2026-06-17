using eVote360.Core.Domain.Entities.Candidate;
using eVote360.Core.Domain.Entities.Election;
using eVote360.Core.Domain.Entities.ElectivePosition;

namespace eVote360.Core.Domain.Entities.Elector.Vote
{
    public sealed class Votes
    {
        public Guid Id { get; init; }
        public required int IdElection { get; init; }
        public required int IdElectivePosiction { get; init; }
        public int? IdCandidate { get; init; }
        public ElectivePositions? ElectivePosition { get; init; }
        public Entities.Election.Election? Elections { get; init; }
        public Candidates? Candidacte {  get; init; }


    }
}
