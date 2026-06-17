using eVote360.Core.Application.Contracts.Authentication.Command;
using eVote360.Core.Application.Contracts.Candidate.Commands;
using eVote360.Core.Application.Contracts.Services;
using eVote360.Core.Application.DTOs.Candidates;
using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.Candidate;

using eVote360.Core.Domain.Settings.ValueObjects.Candidate;

using eVote360.Core.Domain.Validators.CandidateValidator;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Services.Candidate.CommandHandler
{
    public class CandidateUpdate : ICandidateUpdateCommand
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly ICandidateValidator _candidateValidator;
        private readonly IFileStorageService _fileStorageService;
        private readonly ISessionUser _sessionUser;
        public List<Domain.Common.Errors.Error> _errors = new List<Domain.Common.Errors.Error>();

        public CandidateUpdate(
            ICandidateRepository candidateRepository, 
            ICandidateValidator candidateValidator,
            IFileStorageService fileStorageService,
            ISessionUser sessionUser
            )
        {
            _candidateRepository = candidateRepository;
            _candidateValidator = candidateValidator;
            _fileStorageService = fileStorageService;
            _sessionUser = sessionUser;
        }

        public async Task<ValidationResult> UpdateCandidateAsync(UpdateCandidateDto dto, int PartyId)
        {
            try
            {
                if (dto == null) return ValidationResult.Failure(CandidatesError.DataInvalid);

                var candidateById = await _candidateRepository.GetByIdEntitie(dto.Id);
                if (candidateById == null)
                    return ValidationResult.Failure(CandidatesError.DataInvalid);

                
                if (candidateById.PoliticalPartyId != PartyId)
                    return ValidationResult.Failure(CandidatesError.CandidateNotBelongsToParty);

                var validation = await _candidateValidator.ValidateUpdateAsync(dto.Id, dto.Name, dto.LastName, PartyId);
                if (!validation.IsValid) return validation;

                candidateById.Name = new FullName(dto.Name, dto.LastName);

                if (dto.PhotoUrl != null)
                {
                    string photoPath = await _fileStorageService.SaveFileAsync(dto.PhotoUrl, "candidates");
                    
                    if (string.IsNullOrWhiteSpace(photoPath))
                        return ValidationResult.Failure(CandidatesError.PhotoInvalid);

                    candidateById.PhotoUrl = new CandidatePhoto(photoPath);
                }

                candidateById.UpdateAt = DateTimeOffset.UtcNow;
                candidateById.UpdateUserId = _sessionUser.GetUserId(); 

                var update = await _candidateRepository.UpdateEntitieAsync(candidateById);
                if (!update) 
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
