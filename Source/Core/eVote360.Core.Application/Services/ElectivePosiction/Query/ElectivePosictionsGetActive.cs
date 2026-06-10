using eVote360.Core.Application.Contracts.ElectivePosictions.QueryServices;
using eVote360.Core.Application.DTOs.ElectivePositions;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Contracts.Repositories.ElectivePosition;

namespace eVote360.Core.Application.Services.ElectivePosiction.Query
{
    public class ElectivePosictionsGetActive :  IElectivePosictionsGetActiveQuery
    {
        private readonly IElectivePositionsRepository _electivePositionsRepository;
        private List<Error> _errors = new List<Error>();

        public ElectivePosictionsGetActive(IElectivePositionsRepository electivePositionsRepository)
        {
            _electivePositionsRepository = electivePositionsRepository; 
        }
        public async Task<IReadOnlyCollection<ElectivePosictionsDto>> GetAllActiveElectivePosictionsAsync()
        {
            try
            {
                //agregar validaciones de autorization  
                var electivesPosictions = await _electivePositionsRepository.GetAllActiveAsync();
                if (electivesPosictions == null)
                {
                    _errors.Add(new Error("Ha ocurrido un error inesperado", "Ha ocurrido un fallo al intentar  obtener las posiciones electivas activas, favor intente más tarde."));
                    return [];
                }

                var electiveListDto = new List<ElectivePosictionsDto>();
                foreach (var itme in electivesPosictions)
                {
                    var dto = new ElectivePosictionsDto()
                    {
                        Id = itme.Id,
                        Name = itme.Name,
                        State = itme.State,
                        Descriptions = itme.Description
                    };
                    electiveListDto.Add(dto);
                }
                return electiveListDto;
            }
            catch (Exception)
            {
                return new List<ElectivePosictionsDto>();
            }
        }
    }
}
