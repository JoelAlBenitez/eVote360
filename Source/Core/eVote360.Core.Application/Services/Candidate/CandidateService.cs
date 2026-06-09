using eVote360.Core.Application.Contracts.Candidate;
using eVote360.Core.Application.DTOs.Candidates;
using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.Candidate;
using eVote360.Core.Domain.Entities.Candidate;
using eVote360.Core.Domain.Entities.Candidate.ValueObjects;
using eVote360.Core.Domain.Validators.CandidateValidator;

namespace eVote360.Core.Application.Services.Candidates
{

   
    public class CandidateService : ICandidateService
    {

        private readonly ICandidateRepository _candidateRepository;
        private readonly ICandidateValidator _candidateValidator;


        public CandidateService(ICandidateRepository candidateRepository, ICandidateValidator candidateValidator) {

            _candidateRepository = candidateRepository;
            _candidateValidator = candidateValidator;
        }

        public async Task<ValidationResult> CreateCandidateAsync(CreateCandidateDto dto, int PartyId)
        {
            try
            {
                if (dto == null) return ValidationResult.Failure(CandidatesError.DataInvalid);

                var candidate = new Candidate
                {
                    Id = 0,
                    Name = new FullName(dto.Name, dto.LastName),
                    PoliticalPartyId = PartyId,
                    IsActive = true,
                    HasParticipatedInElection = false,
                    CreateAt = DateTimeOffset.Now,
                    CreateUserId = 0 // vendrá de la cookieee
                };

                var validation = await _candidateValidator.ValidateCreateAsync(candidate);
                if (!validation.IsValid) return validation;

                var create = await _candidateRepository.CreateEntiteAsync(candidate);
                if (!create) return ValidationResult.Failure(CandidatesError.DataInvalid);

                return ValidationResult.Success();
            }
            catch (Exception ex)
            {
                return ValidationResult.Failure(new Domain.Common.Errors.Error("Error inesperado", ex.Message));
            }
        }
        public Task<ValidationResult> ChangeStateAsync(int candidateId, int PartyId)
        {
            throw new NotImplementedException();
        }

      

        public Task<IEnumerable<CandidateDTO>> GetAllPartyAsync(int PartyId)
        {
            throw new NotImplementedException();
        }

        public Task<CandidateDTO> GetByIdAsync(int candidateId, int partyId)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> UpdateCandidateAsync(UpdateCandidateDto dto, int PartyId)
        {
            throw new NotImplementedException();
        }
    }
}
