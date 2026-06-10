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

                var validation = await _candidateValidator.ValidateCreateAsync(PartyId);
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

        public async Task<ValidationResult> UpdateCandidateAsync(UpdateCandidateDto dto, int PartyId)
        {
            try
            {
                if (dto == null) return ValidationResult.Failure(CandidatesError.DataInvalid);

                var CandidateById = await _candidateRepository.GetByIdEntitie(dto.Id);
                if (CandidateById == null)
                    return ValidationResult.Failure(CandidatesError.DataInvalid);

                var validation = await _candidateValidator.ValidateUpdateAsync(dto.Id, dto.Name, dto.LastName, PartyId);
                if (!validation.IsValid) return validation;

                CandidateById.Name = new FullName(dto.Name, dto.LastName);
                CandidateById.UpdateAt = DateTimeOffset.Now;
                CandidateById.UpdateUserId = 0;

                var update = await _candidateRepository.UpdateEntitieAsync(CandidateById);
                if (!update) return ValidationResult.Failure(CandidatesError.DataInvalid);

                return ValidationResult.Success();
            }
            catch (Exception ex)
            {
                return ValidationResult.Failure(new Domain.Common.Errors.Error("Error inesperado", ex.Message));
            }
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

                var change = await _candidateRepository.DesactiveEntitie(candidateId);
                if (!change) return ValidationResult.Failure(CandidatesError.DataInvalid);

                return ValidationResult.Success();
            }
            catch (Exception ex)
            {
                return ValidationResult.Failure(new Domain.Common.Errors.Error("Error inesperado", ex.Message));
            }

        }

        public async Task<CandidateDTO> GetByIdAsync(int candidateId, int partyId)
        {

            var candidateById = await _candidateRepository.GetByIdEntitie(candidateId);

            if (candidateById == null)
            {
                return null;
            }

            if (candidateById.PoliticalPartyId != partyId) return null;

            var DtoCandidate = new CandidateDTO

            {
     
                Name = candidateById.Name.Name,
                LastName = candidateById.Name.LastName,
                PhotoUrl = candidateById.PhotoUrl?.PhotoUrl,
                State = candidateById.IsActive,
                PoliticalPartyId = candidateById.PoliticalPartyId,
                HasParticipatedInElection = candidateById.HasParticipatedInElection,
                CreateAt = candidateById.CreateAt,
                CreateUserId = candidateById.CreateUserId



            };

            return DtoCandidate;
        }

        public async Task<IEnumerable<CandidateDTO>> GetAllPartyAsync(int PartyId)
        {
            var AllGetCandidateParty = await _candidateRepository.GetAllByPartyIdAsync(PartyId);

            var candidateList = new List<CandidateDTO>();

            foreach (var item in AllGetCandidateParty)
            {
                var candidateDTO = new CandidateDTO
                {
                    Name = item.Name.Name,
                    LastName = item.Name.LastName,
                    PhotoUrl = item.PhotoUrl?.PhotoUrl,
                    State = item.IsActive,
                    PoliticalPartyId = item.PoliticalPartyId,
                    HasParticipatedInElection = item.HasParticipatedInElection,
                    CreateAt = item.CreateAt,
                    CreateUserId = item.CreateUserId

                };
                candidateList.Add(candidateDTO);
            }
            return candidateList;
        }

       

     
    }
}
