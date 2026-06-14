using eVote360.Core.Application.DTOs.Election;
using eVote360.Core.Domain.Common.ValidationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Contracts.Election.Commands
{
    public interface IElectionUpdateCommand
    {
        Task<ValidationResult> ExecuteAsync(ElectionDto election);
    }
}
