using eVote360.Core.Application.Contracts.ElectivePosictions.QueryServices;
using eVote360.Core.Application.DTOs.ElectivePositions;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Contracts.Repositories.ElectivePosition;

namespace eVote360.Core.Application.Services.ElectivePosiction.Query
{
    public class ElectivePosictionsGetAll : IElectivePosictionsGetAllQuery
    {
        private readonly IElectivePositionsRepository _electivePositionsRepository;
   
        private List<Error> _errors = new List<Error>();

        public ElectivePosictionsGetAll(IElectivePositionsRepository electivePositionsRepository)
        {
            _electivePositionsRepository = electivePositionsRepository;
            
        }

        public async Task<IReadOnlyCollection<ElectivePosictionsDto>> GetAllAsync()
        {
            try
            {
                //agregar validaciones de autorization
                //

                var electivesPosictionsDto = await _electivePositionsRepository.GetAllAsync();
                if (electivesPosictionsDto == null) return [];

                var listElectivesPosictionsDto = new List<ElectivePosictionsDto>();
                foreach (var item in electivesPosictionsDto)
                {
                    var elective = new ElectivePosictionsDto()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        State = item.State,
                        Descriptions = item.Description
                    };
                    listElectivesPosictionsDto.Add(elective);
                }
                return listElectivesPosictionsDto;
            }
            catch (Exception)
            {

                return new List<ElectivePosictionsDto>();
            }
        }
    }
}
