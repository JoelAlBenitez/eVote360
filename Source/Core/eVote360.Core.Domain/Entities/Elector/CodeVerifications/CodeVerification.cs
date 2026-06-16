using eVote360.Core.Domain.Entities.Citizens;
using eVote360.Core.Domain.Entities.Election;
namespace eVote360.Core.Domain.Entities.Elector.CodeVerifications
{
    public sealed class CodeVerification
    {
        public required Guid IdCitizens { get; set; }
        public required int IdElection {  get; set; }
        public required int Code {  get; set; }
        public required bool State { get; set; }
        public required DateTimeOffset CreateAt { get; set; }
        public required DateTimeOffset ExpirationDate { get; set; }
        public Citizen? Citizen { get; set; }
        public Election.Election? election { get; set; }
       
    }
}
