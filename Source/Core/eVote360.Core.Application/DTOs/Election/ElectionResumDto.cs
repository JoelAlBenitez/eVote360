using eVote360.Core.Application.DTOs.Base;
using eVote360.Core.Domain.Common.Enums;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace eVote360.Core.Application.DTOs.Election
{
    public record ElectionResumDto
    {

        public required int Id { get; set; }
        public required string Name { get; set; }
        public DateTime ElectionDate { get; set; }
        public ElectionState ElectionState { get; set; }
        public required int NumberParticipatingMatches { get; set; }
        public required int NumberCandidactesParticipating { get; set; }
        public required int NumberCitizenParticipating { get; set; }
    }
}
