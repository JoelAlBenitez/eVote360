using PoliticalPartyEntity = eVote360.Core.Domain.Entities.PoliticalParty.PoliticalParty;


namespace eVote360.Core.Domain.Contracts.ServiceValidates.PoliticalParty
{
    public interface IPoliticalPartyDomainService
    {
        Task<bool> PoliticalPartyNameAlreadyExist(string name);
        Task<bool> ValidateUniqueAcronymAsync(string acronym);
        Task<bool> PoliticalPartyAlreadyParticipated(int id);

        Task<bool> ExistByIdAsync(int id);
        Task<PoliticalPartyEntity?>GetPartyForValidationAsync(int id);
    }
}
