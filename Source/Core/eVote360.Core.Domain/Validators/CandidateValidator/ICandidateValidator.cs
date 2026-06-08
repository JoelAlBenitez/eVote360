using eVote360.Core.Domain.Entities.Candidate;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eVote360.Core.Domain.Common.ValidationResult;
namespace eVote360.Core.Domain.Validators.CandidateValidator
{
    public interface ICandidateValidator
    {

        Task<ValidationResult> ValidateCreateAsync(Candidates candidate);
        Task<ValidationResult> ValidateUpdateAsync(Candidates candidate);
        Task<ValidationResult> ValidateChangeStateAsync(Candidates candidate);
    }
}
