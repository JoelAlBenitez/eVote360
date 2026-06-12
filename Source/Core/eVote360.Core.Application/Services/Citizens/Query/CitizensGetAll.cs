

using eVote360.Core.Application.Contracts.Citizens.Query;
using eVote360.Core.Application.DTOs.Citizens;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Contracts.Repositories.Citizens;

namespace eVote360.Core.Application.Services.Citizens.Query
{
    public sealed class CitizensGetAll : ICitizensGetAllQuery
    {

        private readonly ICitizenRepository _citizenRepository;
        private List<Error> _errors = new List<Error>();

        public CitizensGetAll(ICitizenRepository citizenRepository) {
            
            _citizenRepository = citizenRepository;
        }

        public async Task<IReadOnlyCollection<CitizensDto>> GetAllAsync()
        {
            try
            {
                var citizens = await _citizenRepository.GetAll();
                if (citizens == null) return [];

                var dtos = new List<CitizensDto>();

                foreach (var item in citizens)
                {
                    var dto = new CitizensDto { 
                        Id = item.Id,
                        Identification = item.IdentificationNumber.Value,
                        Email = item.Email.Value,
                        LastName = item.LastName,
                        Name = item.Name,
                        State = item.State,
                    
                    };    
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch(Exception)
            {
                return new List<CitizensDto>();
            }
        }
    }
}
