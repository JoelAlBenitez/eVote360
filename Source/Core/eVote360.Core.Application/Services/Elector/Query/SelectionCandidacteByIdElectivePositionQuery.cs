
using eVote360.Core.Application.Contracts.Elector.Query;
using eVote360.Core.Application.DTOs.Elector.Dashboard;
using eVote360.Core.Domain.Contracts.Repositories.Elector.SelectPorcess;

namespace eVote360.Core.Application.Services.Elector.Query
{
    public class SelectionCandidacteByIdElectivePositionQuery : ISelectionCandidateByIdElectivePosictionQuery
    {

        private readonly ISelectDataForElectoralProcessRepository _selectDataForElectoralProcessRepository;

        public SelectionCandidacteByIdElectivePositionQuery(
            ISelectDataForElectoralProcessRepository selectDataForElectoralProcessRepository)
        {
            _selectDataForElectoralProcessRepository = selectDataForElectoralProcessRepository;
        }

        public async Task<IReadOnlyCollection<SelectionCandidacteDto>> GetSelectionCandidacteAsync(int IdElectivePosition)
        {
            try
            {
                var listSelection = await _selectDataForElectoralProcessRepository.GetElectorSelectCandidacteElectivepPosictionsAsync(IdElectivePosition);
                if (listSelection == null) return [];
                var listDto =  listSelection.Select(s => new SelectionCandidacteDto
                {
                    IdCandidacte = s.IdCandidate,
                    NameCandidacte = s.NameCandidacte,
                    PhotoCandidacte = s.PhotoUrlOfCandidacte,
                    PoliticalParty = s.PoliticalParty,
                    PoliticalPartyLogoUrl = s.PhotoUrlOfCandidacte
                }).ToList();

                return listDto;
            }
            catch (Exception)
            {
                return new List<SelectionCandidacteDto>();
            }
        }
    }
}
