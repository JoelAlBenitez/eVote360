using eVote360.Core.Application.DTOs.Elector.Identification;
using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Application.Contracts.Elector.Commands.Identification
{
    public interface IIdentificationVerifyCompareIdentificationByImg
    {
        Task<ValidationResult> VerifyIdentificationComparedByImg(IdentificiationDto identificiationDto);
    }
}
