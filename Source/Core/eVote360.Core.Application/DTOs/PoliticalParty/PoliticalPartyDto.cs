using eVote360.Core.Application.DTOs.Base;


namespace eVote360.Core.Application.DTOs.PoliticalParty
{
    public record PoliticalPartyDto : BaseDto<int>
    {
        public required string PoliticalPartyDescription { get; set; }
        public required string PoliticalPartyAcronym { get; set; }
        public required string PoliticalPartyLogo { get; set; }
    }
}
