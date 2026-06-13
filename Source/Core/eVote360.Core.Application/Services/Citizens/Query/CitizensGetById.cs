

using eVote360.Core.Application.Contracts.Citizens.Query;
using eVote360.Core.Application.DTOs.Citizens;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.Citizens;

namespace eVote360.Core.Application.Services.Citizens.Query
{
    public sealed class CitizensGetById : ICitizensGetByIdQuery
    {

        private readonly ICitizenRepository _citizenRepository;
        private List<Error> _errors = new List<Error>();

        public CitizensGetById(ICitizenRepository citizenRepository)
        {
            _citizenRepository = citizenRepository;
        }

        public async Task<ValidationResult<CitizensDto>> GetByIdAsync(Guid Id)
        {
            try
            {

                var citizens = await _citizenRepository.GetByIdEntitie(Id);
                if(citizens == null)
                {
                    _errors.Add(new Error("Ha ocurrido un error en la obtención del registro", "Registro no encontrado o no procesado favor intente de nuevo más tarde."));
                    ValidationResult<CitizensDto>.Failure(_errors);
                }
                var dto = new CitizensDto { 
                    Id = citizens!.Id,
                    Identification = citizens.IdentificationNumber.Value,
                    Email = citizens.Email.Value,
                    Name = citizens.Name,
                    LastName = citizens.LastName,
                    State = citizens.State,
            
                };
                return ValidationResult<CitizensDto>.Success(dto);
            }
            catch (Exception ex) {

                _errors.Add(new Error("Ha ocurrido un error inesperado", ex.Message));
                return ValidationResult<CitizensDto>.Failure(_errors);
            }
        }
    }
}
