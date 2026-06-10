using eVote360.Core.Application.Contracts.Candidate.Commands;
using eVote360.Core.Application.Contracts.Services;
using eVote360.Core.Application.DTOs.Candidates;
using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.Candidate;
using eVote360.Core.Domain.Entities.Candidate;
using eVote360.Core.Domain.Entities.Candidate.ValueObjects;
using eVote360.Core.Domain.Validators.CandidateValidator;

namespace eVote360.Core.Application.Services.Candidate.CommandHandler
{
    public class CandidateCreate : ICandidateCreateCommand
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly ICandidateValidator _candidateValidator;
        private readonly IFileStorageService _fileStorageService;
        public List<Domain.Common.Errors.Error> _errors = new List<Domain.Common.Errors.Error>();

        public CandidateCreate(
            ICandidateRepository candidateRepository, 
            ICandidateValidator candidateValidator,
            IFileStorageService fileStorageService)
        {
            _candidateRepository = candidateRepository;
            _candidateValidator = candidateValidator;
            _fileStorageService = fileStorageService;
        }

        public async Task<ValidationResult> CreateCandidateAsync(CreateCandidateDto dto, int PartyId)
        {
            try
            {
                if (dto == null) return ValidationResult.Failure(CandidatesError.DataInvalid);

                string photoPath = "";
                if (dto.PhotoUrl != null)
                {
                    photoPath = await _fileStorageService.SaveFileAsync(dto.PhotoUrl, "candidates");
                }

                var candidate = new eVote360.Core.Domain.Entities.Candidate.Candidate
                {
                    Name = new FullName(dto.Name, dto.LastName),
                    PhotoUrl = new CandidatePhoto(photoPath),
                    PoliticalPartyId = PartyId,
                    State = true,
                    HasParticipatedInElection = false
                };

                var validation = await _candidateValidator.ValidateCreateAsync(PartyId);
                if (!validation.IsValid) return validation;

                var create = await _candidateRepository.CreateEntiteAsync(candidate);
                if (!create) 
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
