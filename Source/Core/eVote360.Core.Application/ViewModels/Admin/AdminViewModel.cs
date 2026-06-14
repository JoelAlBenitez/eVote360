namespace eVote360.Core.Application.ViewModels.Admin
{
    public sealed class AdminViewModel
    {
        public required string UserName { get; set; }
        public required int NumberOfRegisteredCitizens { get; set; }
        public required int NumberOfElections { get; set; }
        public required int NumberOfMatches { get; set; }
        public required int NumberOfCandidates { get; set; }

    }
}
