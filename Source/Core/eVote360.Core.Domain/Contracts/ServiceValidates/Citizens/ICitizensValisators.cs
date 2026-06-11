namespace eVote360.Core.Domain.Contracts.ServiceValidates.Citizens
{
    public interface ICitizensValisators
    {
        Task<bool> ExistCitizensByIdentification(string Identification);
        Task<bool> ExistCitizensByEmail (string Email);
        Task<bool> ExistOtherCitizens(Guid Id, string Identification);
        Task<bool> ExistOtherCitizensByState(Guid Id, string Identification, bool state);
        Task<bool> CitizenParticipatedInElection(Guid Id, string Identification);
    }
}
