

using eVote360.Core.Domain.Common.Enums;

namespace eVote360.Core.Domain.Entities.Election
{
    public  class ElectionResum
    {

        public required int Id { get; set; }
        public required string NameElection { get; set; }
        public required DateTime DateRealized { get; set; }
        public required ElectionState State { get; set; }
        public required int NumberParticipatingMatches { get; set; }
        public required int NumberElectivePositionsParticipating { get; set; }
        public required int NumberCitizenParticipating { get; set; }

    }
}
