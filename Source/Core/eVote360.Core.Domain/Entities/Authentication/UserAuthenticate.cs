using eVote360.Core.Domain.Common.Enums;

namespace eVote360.Core.Domain.Entities.Authentication
{
    public class UserAuthenticate
    {
        public required int IdUser {  get; set; }
        public required string NameUser { get; set; }
        public required UserRole Role {  get; set; }
        public int? PoliticalPartyId { get; set; }
        public required bool state {  get; set; }
    }
}
