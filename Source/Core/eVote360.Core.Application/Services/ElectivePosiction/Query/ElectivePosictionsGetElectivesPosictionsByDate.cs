
using eVote360.Core.Application.Contracts.ElectivePosictions.Query;
using eVote360.Core.Application.DTOs.ElectivePositions;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Contracts.Repositories.ElectivePosition;

namespace eVote360.Core.Application.Services.ElectivePosiction.Query
{
    public class ElectivePosictionsGetElectivesPosictionsByDate : IElectivePosictionsGetElectivesPosictionsByDateQuery
    {

        private readonly IElectivePositionsRepository _electivePositionsRepository;
        private List<Error> _errors = new List<Error>();

        public ElectivePosictionsGetElectivesPosictionsByDate(IElectivePositionsRepository electivePositionsRepository)
        {
            _electivePositionsRepository = electivePositionsRepository;
          
        }

        public async Task<IReadOnlyCollection<ElectivePosictionsDto>> GetElectivePosictionsByDate(DateTimeOffset dateStart, DateTimeOffset dateEnd)
        {
            try
            {
                var electivesP = await _electivePositionsRepository.GetAllDateAsync(dateStart, dateEnd);
                if (electivesP == null) return [];

                var electivesPDto = new List<ElectivePosictionsDto>();
                foreach (var election in electivesP)
                {
                    var dto = new ElectivePosictionsDto()
                    {
                        Id = election.Id,
                        Name = election.Name,
                        Descriptions = election.Description,
                        State = election.State
                    };

                    electivesPDto.Add(dto);
                }
                return electivesPDto;

            }
            catch (Exception) { return new List<ElectivePosictionsDto>(); }
        }
    }
}
