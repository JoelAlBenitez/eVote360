using System;
using eVote360.Core.Domain.Entities.PoliticalAlliances;

namespace eVote360.Core.Application.Alliances.DTOs
{
    public record AllianceDto
    {
        public int Id { get; init; }
        public int RequestingPartyId { get; init; }
        public int ReceivingPartyId { get; init; }
        public AllianceStatus Status { get; init; }
        public DateTime RequestDate { get; init; }
        public DateTime? AcceptedDate { get; init; }

        public string? RequestingPartyName { get; init; }
        public string? RequestingPartySiglas { get; init; }
        public string? ReceivingPartyName { get; init; }
        public string? ReceivingPartySiglas { get; init; }
    }
}
