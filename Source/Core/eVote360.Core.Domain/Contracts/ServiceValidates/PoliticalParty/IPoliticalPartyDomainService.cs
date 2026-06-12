using eVote360.Core.Domain.Contracts.Repositories.PoliticalParty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Contracts.ServiceValidates.PoliticalParty
{
    public interface IPoliticalPartyDomainService : IPoliticalPartyRepository
    {
        Task<bool> PoliticalPartyNameAlreadyExist(string name);
        Task<bool> ValidateUniqueAcronymAsync(string acronym);
        Task<bool> PoliticalPartyAlreadyParticipated(int id);
    }
}
