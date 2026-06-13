using eVote360.Core.Domain.Entities.ElectivePosition;

namespace eVote360.Core.Domain.Entities.Elector.Vote
{
    public sealed class Votes
    {
        public Guid Id { get; }
        public required int IdElection { get; init; }
        public required int IdElectivePosiction { get; init; }
        public required int IdCandidate { get; init; }
        public ElectivePositions? ElectivePosition { get; init; }

        //agregar entidad virtual de elecciones y candidatos -> agregar IColleccion en puestos electivos
    }
}
