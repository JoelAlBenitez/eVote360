
using eVote360.Core.Application.Contracts.Admin.Query;
using eVote360.Core.Application.DTOs.Admin;
using eVote360.Core.Domain.Contracts.Repositories.AdminManager;

namespace eVote360.Core.Application.Services.Admin
{
    public class CountRegisterQueryHandler : ICountRegisterAdminQuery
    {
        private readonly IAdminManagerRepository _adminManagerRepository;

        public CountRegisterQueryHandler(IAdminManagerRepository adminManagerRepository)
        {
            _adminManagerRepository = adminManagerRepository;
        }

        public async Task<AdminCountDto> CountRegisterQueryAsync()
        {
            try
            {
                var citizen = await _adminManagerRepository.CountCitizensRegisterAsync();
                var election = await _adminManagerRepository.CountElectionsRegisterAsync();
                var political = await _adminManagerRepository.PoliticalPartyAsync();

                return new AdminCountDto { CountCitizensRegisterAsync = citizen,
                    CountElectionsRegisterAsync = election, PoliticalPartyAsync = political};

            }
            catch (Exception )
            {
                return new AdminCountDto { CountCitizensRegisterAsync = 0, CountElectionsRegisterAsync  = 0, PoliticalPartyAsync = 0};
            }
        }
    }
}
