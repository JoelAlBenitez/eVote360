using eVote360.Core.Domain.Commom.BaseEntity;
using eVote360.Core.Domain.Entities.Elector.AuditVote;
using eVote360.Core.Domain.Entities.Elector.Vote;
using eVote360.Core.Domain.Settings.ValueObjects.Emails;
using eVote360.Core.Domain.Settings.ValueObjects.Identifications;

namespace eVote360.Core.Domain.Entities.Citizens
{
    public class Citizen : BaseEntitie<Guid, string>
    {
        public required string LastName { get; set; }
        public required Email Email { get; set; }
        public required IdentificationN IdentificationNumber { get; set; }
        public ICollection<AuditVotes>? AuditVote {  get; set; }
    }
}
