using eVote360.Core.Application.Contracts.Elector.Query;
using eVote360.Core.Application.DTOs.Elector.Dashboard;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Contracts.Repositories.Elector.SelectPorcess;

namespace eVote360.Core.Application.Services.Elector.Query
{
    public class WindowElectivePositionQuery : IWindowElectivePositionQuery
    {

        private readonly ISelectDataForElectoralProcessRepository _selectDataForElectoralProcessRepository;

        public WindowElectivePositionQuery(ISelectDataForElectoralProcessRepository selectDataForElectoralProcessRepository)
        {
            _selectDataForElectoralProcessRepository = selectDataForElectoralProcessRepository;
        }

        public async Task<IReadOnlyCollection<WindowsElectivePositionsDto>> GetWindowsElectivePositionsAsync()
        {
            try
            {
                var electivePostions = await _selectDataForElectoralProcessRepository.GetElectorDataElectionPositionsAsync();
                if (electivePostions == null) return [];
                var listDto = electivePostions.Select(e => new WindowsElectivePositionsDto
                {
                    IdElectivePosition = e.IdElectivePosition,
                    NumberActualCandidactes = e.NumberActualCandidacte,
                    NumberPoliticalParty = e.NumberPoliticalPartyParticiped,
                    NameElectivePosition = e.NameElectivePosiction,
                    IsSelection = false

                }).ToList();

                return listDto;
            }
            catch (Exception ) {
                return new List<WindowsElectivePositionsDto>();
            }
        }
    }
}
