using eVote360.Core.Application.Contracts.ElectivePosictions;
using eVote360.Core.Application.DTOs.ElectivePositions;
using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Application.Services.ElectivePosictions
{
    public class ElectivePosictions : IElectivePosictionsService
    {
        public Task<ValidationResult> AlterState(ElectivePosictionsDesactiveOrActive dto)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> CreateAsync(ElectivePosictionsDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ElectivePosictionsDto> GetAllActiveElectivePosictionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ElectivePosictionsDto> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ElectivePosictionsDto> GetAllById()
        {
            throw new NotImplementedException();
        }

        public Task<ElectivePosictionsDto> GetElectivePosictionsByDate(DateTimeOffset dateStart, DateTimeOffset dateEnd)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> UpdateAsync(ElectivePosictionsDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
