namespace eVote360.Core.Domain.Contracts.ServiceValidates.Citizens
{
    public interface ICitizensServiceValidate
    {
        Task<bool> ExistCitizensByIdentification(string Identification);
        Task<bool> ExistCitizensByEmail (string Email);
        Task<bool> ExistOtherCitizens(Guid Id, string Identification);
        Task<bool> CurrentStateOfTheCitizen(Guid Id);
        Task<bool> ExistOtherCitizensByState(Guid Id, string Identification, bool state);
        Task<bool> CitizenParticipatedInElection(Guid Id, string Identification);
    }
}
