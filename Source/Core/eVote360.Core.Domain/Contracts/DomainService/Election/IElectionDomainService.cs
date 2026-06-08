using eVote360.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Contracts.DomainService.Election
{
    public interface IElectionDomainService
    {
        Task<bool> ExistElectionByName(string Name);
        Task<bool> ExistElectionById(int idElection);
        Task<bool> ElectionHasEnoughParties (int idElection);
        Task<bool> ExistActiveElection();
        Task<bool> ValidElectionDate(ElectionDate electionDate);
    }
}
