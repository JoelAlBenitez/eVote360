using eVote360.Core.Domain.Entities.Citizens;
using ElectionEntitie = eVote360.Core.Domain.Entities.Election.Election;

namespace eVote360.Core.Domain.Entities.Elector.AuditVote
{
    public sealed class AuditVotes
    {
        public required Guid Id { get; init; }
        public required Guid IdCitizen {get; init;} 
        public required int IdElection { get; init;}
        public required DateTimeOffset CreatAt {  get; init;}

        public Citizen? Citizens { get; init; }

        //agregar entidad vitual de elecciones

        public ElectionEntitie? ElectionEntitie { get; init; }
    }
}
