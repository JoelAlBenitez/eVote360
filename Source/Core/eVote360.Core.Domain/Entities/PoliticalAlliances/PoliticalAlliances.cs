using eVote360.Core.Domain.Common.Enums;
using PoliticalPartyEntity = eVote360.Core.Domain.Entities.PoliticalParty.PoliticalParty;
using System;

namespace eVote360.Core.Domain.Entities.PoliticalAlliances
{
    public class PoliticalAlliances
    {
        public int Id { get; set; }
        public int RequestingPartyId { get; set; }
        public int ReceivingPartyId { get; set; }
        public AllianceStatus Status { get; set; }

        public DateTimeOffset RequestDate { get; set; }
        public DateTimeOffset? ResponseDate { get; set; }
        public DateTimeOffset CreateAt { get; set; }
        public int CreateUserId { get; set; }

        public void Accept()
        {
            Status = AllianceStatus.Accepted;
            ResponseDate = DateTimeOffset.Now;
        }

        public void Reject()
        {
            Status = AllianceStatus.Rejected;
            ResponseDate = DateTimeOffset.Now;
        }


        // Propiedades de navegación - descomentar cuando los modulos ten ready

        // Propiedades de navegación - descomentar cuando todos los módulos estén en development

        public PoliticalPartyEntity RequestingParty { get; set; }
        public PoliticalPartyEntity ReceivingParty { get; set; }
    }
}
