namespace eVote360.Core.Application.DTOs.Admin
{
    public class AdminDto
    {
        public required string UserName { get; set; }
        public required int CountElectionsRegisterAsync { get; set; }
        public required int PoliticalPartyAsync { get; set; }
        public required int CountCitizensRegisterAsync { get; set; }
        public required int CountCandidacteRegisterAsync { get; set; }
    }
}
