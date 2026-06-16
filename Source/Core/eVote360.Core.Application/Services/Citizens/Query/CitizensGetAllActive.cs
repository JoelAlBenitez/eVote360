using eVote360.Core.Application.Contracts.Citizens.Query;
using eVote360.Core.Application.DTOs.Citizens;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Contracts.Repositories.Citizens;

namespace eVote360.Core.Application.Services.Citizens.Query
{
    public sealed class CitizensGetAllActive : ICitizensGetActiveQuery
    {

        private List<Error> _errros = new List<Error>();
        private readonly ICitizenRepository _citizenRepository;

        public CitizensGetAllActive(ICitizenRepository citizenRepository)
        {
            _citizenRepository = citizenRepository;
        }


        public async Task<IReadOnlyCollection<CitizensDto>> GetActiveAsync()
        {
            try
            {
                var citizens = await _citizenRepository.GetActiveCitizensByActive();
                if (citizens == null) return [];

                var dtos = new List<CitizensDto>();
                foreach (var item in citizens)
                {
                    var dto = new CitizensDto { 
                        Id = item.Id,
                        Identification = item.IdentificationNumber.Value,
                        Email = item.Email.Value,
                        Name = item.Name,
                        LastName = item.LastName,
                        State = item.State
             
                    };
                    dtos.Add(dto);
                }
                return dtos;

            }
            catch (Exception) {

                return new List<CitizensDto>();
            }
        }
    }
}
