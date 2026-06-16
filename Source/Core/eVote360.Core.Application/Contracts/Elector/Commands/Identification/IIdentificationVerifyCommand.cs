using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Application.DTOs.Elector.Identification;
namespace eVote360.Core.Application.Contracts.Elector.Commands.Identification
{
    public interface IIdentificationVerifyCommand
    {
        Task<ValidationResult> VerifyIdentificationByCitizen(IdentificiationDto identificiationDto);
    }
}
