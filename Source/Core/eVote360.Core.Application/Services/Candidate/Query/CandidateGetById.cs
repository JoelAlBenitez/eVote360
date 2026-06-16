using eVote360.Core.Application.Contracts.Candidate.Query;
using eVote360.Core.Application.DTOs.Candidates;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.Candidate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Services.Candidate.Query
{
    public class CandidateGetById : ICandidateGetByIdQuery
    {
        private readonly ICandidateRepository _candidateRepository;
        private List<Error> _errors = new List<Error>();

        public CandidateGetById(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<ValidationResult<CandidateDTO>> GetByIdAsync(int candidateId, int partyId)
        {
            try
            {
                var candidateById = await _candidateRepository.GetByIdEntitie(candidateId);

                if (candidateById == null)
                {
                    _errors.Add(new Error("No Encontrado", "No se pudo obtener el candidato solicitado."));
                    return ValidationResult<CandidateDTO>.Failure(_errors);
                }

                if (candidateById.PoliticalPartyId != partyId)
                {
                    _errors.Add(new Error("Acceso Denegado", "El candidato no pertenece a su partido político."));
                    return ValidationResult<CandidateDTO>.Failure(_errors);
                }

                var DtoCandidate = new CandidateDTO
                {
                    Id = candidateById.Id,
                    Name = candidateById.Name.Name!,
                    LastName = candidateById.Name.LastName!,
                    PhotoUrl = candidateById.PhotoUrl?.PhotoUrl,
                    State = candidateById.State,
                    PoliticalPartyId = candidateById.PoliticalPartyId,
                    HasParticipatedInElection = candidateById.HasParticipatedInElection
                };

                return ValidationResult<CandidateDTO>.Success(DtoCandidate);
            }
            catch (Exception ex)
            {
                _errors.Add(new Error("Error inesperado", ex.Message));
                return ValidationResult<CandidateDTO>.Failure(_errors);
            }
        }
    }
}
