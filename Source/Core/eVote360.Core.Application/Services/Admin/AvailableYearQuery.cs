using eVote360.Core.Application.Contracts.Admin.Query;
using eVote360.Core.Application.DTOs.Admin;
using eVote360.Core.Domain.Contracts.Repositories.AdminManager;

namespace eVote360.Core.Application.Services.Admin
{
    public class AvailableYearQuery : IAvailableYearsQuery
    {

        private readonly IAdminManagerRepository _adminManagerRepository;

        public AvailableYearQuery(IAdminManagerRepository adminManagerRepository)
        {
            _adminManagerRepository = adminManagerRepository;
        }

        public async Task<IReadOnlyCollection<AdminDate>> AvailableYearAsync()
        {
            try
            {
                var list = await _adminManagerRepository.GetYears();
                var dtosList = new List<AdminDate>();
                foreach (var date in list) {
                
                    var dto = new AdminDate { YearElection = date};
                    dtosList.Add(dto);
                }
                if (dtosList == null) return [];
                return dtosList;
            }
            catch (Exception)
            {
                return new List<AdminDate>();
            }
        }
    }
}
