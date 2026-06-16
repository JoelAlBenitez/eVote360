using eVote360.Core.Application.DTOs.Election;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Contracts.Election.Query
{
    public interface IElectionGetAllQuery
    {
        Task<IReadOnlyCollection<ElectionDto>> ExecuteAsync();
    }
}
