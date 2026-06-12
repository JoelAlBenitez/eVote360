using eVote360.Core.Domain.Entities.PoliticalAlliances;

namespace eVote360.Core.Application.DTOs.PoliticalAlliance
{
    public record AllianceResponseDto
    {
        public int AllianceId { get; set; }
        public AllianceStatus Status { get; set; }
    }
}