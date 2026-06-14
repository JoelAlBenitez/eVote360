using eVote360.Core.Application.Contracts.Candidate.Commands;
using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.Candidate;
using eVote360.Core.Domain.Validators.CandidateValidator;

namespace eVote360.Core.Application.Services.Candidate.CommandHandler
{
    public class CandidateChangeState : ICandidateChangeStateCommand
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly ICandidateValidator _candidateValidator;
        public List<Domain.Common.Errors.Error> _errors = new List<Domain.Common.Errors.Error>();

        public CandidateChangeState(ICandidateRepository candidateRepository, ICandidateValidator candidateValidator)
        {
            _candidateRepository = candidateRepository;
            _candidateValidator = candidateValidator;
        }

        public async Task<ValidationResult> ChangeStateAsync(int candidateId, int PartyId)
        {
            try
            {
                var candidateById = await _candidateRepository.GetByIdEntitie(candidateId);
                if (candidateById == null)
                    return ValidationResult.Failure(CandidatesError.DataInvalid);

                if (candidateById.PoliticalPartyId != PartyId)
                    return ValidationResult.Failure(CandidatesError.CandidateNotBelongsToParty);

                var validation = await _candidateValidator.ValidateChangeStateAsync(candidateId);
                if (!validation.IsValid) return validation;

                var change = await _candidateRepository.AlterState(candidateId, !candidateById.State);
                if (!change) 
                {
                    _errors.Add(CandidatesError.DataInvalid);
                    return ValidationResult.Failure(_errors);
                }

                return ValidationResult.Success();
            }
            catch (Exception ex)
            {
                _errors.Add(new Domain.Common.Errors.Error("Error inesperado", ex.Message));
                return ValidationResult.Failure(_errors);
            }
        }
    }
}
