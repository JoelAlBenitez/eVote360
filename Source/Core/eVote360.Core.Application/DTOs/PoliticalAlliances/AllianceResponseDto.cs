using eVote360.Core.Domain.Entities.PoliticalAlliances;

using eVote360.Core.Domain.Common.Enums;

namespace eVote360.Core.Application.DTOs.PoliticalAlliances
{
    public record AllianceResponseDto
    {
        public int AllianceId { get; set; }
        public AllianceStatus Status { get; set; }
    }
}