using eVote360.Core.Application.Contracts.Admin.Query;
using eVote360.Core.Application.DTOs.Admin;
using eVote360.Core.Domain.Contracts.Repositories.AdminManager;

namespace eVote360.Core.Application.Services.Admin
{
    public class ElectionByYear : IElectionByYearQuery
    {

        private readonly IAdminManagerRepository _adminManagerRepository;

        public ElectionByYear(IAdminManagerRepository adminManagerRepository)
        {
            _adminManagerRepository = adminManagerRepository;
        }

        public async Task<IReadOnlyCollection<AdminDto>> GetRegisterAsync(DateTime year)
        {
            try
            {
                var dtoList = new List<AdminDto>();
                var list =  await _adminManagerRepository.ElectionByYearAsync(year);

                foreach (var item in list) {

                    var dto = new AdminDto { 
                        DateRealized
                        = item.DateRealized,
                        NameElection = item.NameElection,
                        NumberCandidactesParticipating = item.NumberCandidactesParticipating,
                        NumberCitizenParticipating = item.NumberCitizenParticipating,
                        NumberParticipatingMatches = item.NumberParticipatingMatches,
                    };
                    dtoList.Add(dto);
                }
                if (dtoList == null) return [];

                return dtoList;
            }catch (Exception )
             {
                return new List<AdminDto>();
             }
        }
    }
}
