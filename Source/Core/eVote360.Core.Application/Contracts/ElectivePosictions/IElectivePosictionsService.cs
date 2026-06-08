using eVote360.Core.Application.DTOs.ElectivePositions;

namespace eVote360.Core.Application.Contracts.ElectivePosictions
{
    public interface IElectivePosictionsService 
    {
        Task<bool> CreateAsync(ElectivePosictionsDto dto);
        Task<bool> UpdateAsync(ElectivePosictionsDto dto);
        Task<bool> AlterState(ElectivePosictionsDesactiveOrActive dto);
        Task<ElectivePosictionsDto> GetAllActiveElectivePosictionsAsync();
        Task<ElectivePosictionsDto> GetAllAsync();
        Task<ElectivePosictionsDto> GetAllById();
        Task<ElectivePosictionsDto> GetElectivePosictionsByDate(DateTimeOffset dateStart, DateTimeOffset dateEnd);
        //agregar get de obtencion de candidactos asociados a un puesto electivo x
    }
}
