
using eVote360.Core.Application.Contracts.Admin.Query;
using eVote360.Core.Application.Contracts.Authentication.Command;
using eVote360.Core.Application.DTOs.Admin;
using eVote360.Core.Domain.Contracts.Repositories.AdminManager;
using eVote360.Core.Domain.Entities.Candidate;

namespace eVote360.Core.Application.Services.Admin
{
    public class CountRegisterQueryHandler : ICountRegisterAdminQuery
    {
        private readonly IAdminManagerRepository _adminManagerRepository;
        private readonly ISessionUser _xsessionUser;

        public CountRegisterQueryHandler(IAdminManagerRepository adminManagerRepository,
            ISessionUser sessionUser
            )
        {
            _adminManagerRepository = adminManagerRepository;
            _xsessionUser = sessionUser;
        }

        public async Task<AdminDto> CountRegisterQueryAsync()
        {
            try
            {
                var citizen = await _adminManagerRepository.CountCitizensRegisterAsync();
                var election = await _adminManagerRepository.CountElectionsRegisterAsync();
                var political = await _adminManagerRepository.PoliticalPartyAsync();
                var candidactes = await _adminManagerRepository.CountCandidactsRegisterAsync();

                return new AdminDto { CountCitizensRegisterAsync = citizen,
                    CountElectionsRegisterAsync = election, PoliticalPartyAsync = political,
                    CountCandidacteRegisterAsync = candidactes, UserName = _xsessionUser.GetUserName()
                 };

            }
            catch (Exception )
            {
                return new AdminDto { CountCitizensRegisterAsync = 0, 
                    CountElectionsRegisterAsync  = 0,
                    PoliticalPartyAsync = 0, 
                    UserName = "DESCONOCIDO",
                     CountCandidacteRegisterAsync = 0
                };
            }
        }
    }
}
