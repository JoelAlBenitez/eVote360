namespace eVote360.Core.Application.DTOs.PoliticalLeaderAssignment
{
    public class PartyDropdownDto
    {
        public int PartyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Siglas { get; set; } = string.Empty;
    }
}
