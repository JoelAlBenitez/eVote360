using eVote360.Core.Application.Contracts.ElectivePosictions.QueryServices;
using eVote360.Core.Application.DTOs.ElectivePositions;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.ElectivePosition;

namespace eVote360.Core.Application.Services.ElectivePosiction.Query
{
    public class ElectivePosictionsGetById : IElectivePosictionsGetByIdQuery
    {
        private readonly IElectivePositionsRepository _electivePositionsRepository;
   
        private List<Error> _errors = new List<Error>();


        public ElectivePosictionsGetById(IElectivePositionsRepository electivePositionsRepository)
        {
            _electivePositionsRepository = electivePositionsRepository;
        }

        public async Task<ValidationResult<ElectivePosictionsDto>> GetAllById(int Id)
        {
            try
            {
                //agregar validaciones de autorization 

                var elective = await _electivePositionsRepository.GetByIdEntitie(Id);
                _errors.Add(new Error("Ha ocurrido un error inesperado", "Ha ocurrido un error al intentar obtener los datos de este registro."));
                if (elective == null) return ValidationResult<ElectivePosictionsDto>.Failure(_errors);

                var dto = new ElectivePosictionsDto()
                {
                    Id = Id,
                    Name = elective.Name,
                    Descriptions = elective.Description,
                    State = elective.State
                };

                return ValidationResult<ElectivePosictionsDto>.Success(dto);
            }
            catch (Exception ex)
            {
                _errors.Add(new Error("Ha ocurrido un error en la comunicación", ex.Message));
                return ValidationResult<ElectivePosictionsDto>.Failure(_errors);
            }
        }
    }
}
