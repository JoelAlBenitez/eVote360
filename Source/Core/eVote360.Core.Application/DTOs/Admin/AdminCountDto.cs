namespace eVote360.Core.Application.DTOs.Admin
{
    public class AdminCountDto
    {
        public required int CountElectionsRegisterAsync { get; set; }
        public required int PoliticalPartyAsync { get; set; }
        public required int CountCitizensRegisterAsync { get; set; }
    }
}
