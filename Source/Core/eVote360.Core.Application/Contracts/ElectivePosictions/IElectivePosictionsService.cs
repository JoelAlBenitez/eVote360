using eVote360.Core.Application.DTOs.ElectivePositions;
using eVote360.Core.Domain.Commom;
using eVote360.Core.Domain.Common.ValidationResult;
namespace eVote360.Core.Application.Contracts.ElectivePosictions
{
    public interface IElectivePosictionsService 
    {
        Task<ValidationResult> CreateAsync(ElectivePosictionsDto dto);
        Task<ValidationResult> UpdateAsync(ElectivePosictionsDto dto);
        Task<ValidationResult> AlterState(ElectivePosictionsDesactiveOrActive dto);
        Task<ElectivePosictionsDto> GetAllActiveElectivePosictionsAsync();
        Task<ElectivePosictionsDto> GetAllAsync();
        Task<ElectivePosictionsDto> GetAllById();
        Task<ElectivePosictionsDto> GetElectivePosictionsByDate(DateTimeOffset dateStart, DateTimeOffset dateEnd);
        //agregar get de obtencion de candidactos asociados a un puesto electivo x
    }
}
