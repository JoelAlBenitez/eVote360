//using eVote360.Core.Application.Contracts.ElectivePosictions;
//using eVote360.Core.Application.DTOs.ElectivePositions;
//using eVote360.Core.Domain.Common.CodeErrors;
//using eVote360.Core.Domain.Common.Errors;
//using eVote360.Core.Domain.Common.ValidationResult;
//using eVote360.Core.Domain.Contracts.Repositories.ElectivePosition;
//using eVote360.Core.Domain.Entities.ElectivePosition;
//using eVote360.Core.Domain.Validators.ElectivePositionValidator;


//namespace eVote360.Core.Application.Services.ElectivePosiction
//{
//    public class ElectivePosictionsService : IElectivePosictionsService
//    {


//        private readonly IElectivePositionsRepository _electivePosictionRepository;

//        private List<Error> _errors = new List<Error>(); //mejorar este elemento para no hard coderarlo

//        //agregar elemeto de autorization

//        private readonly IElectivePositionsValidator _electivePositionsValidator;

//        public ElectivePosictionsService(IElectivePositionsRepository electivePositionsRepository,
//            IElectivePositionsValidator electivePositionsValidator)
//        {
//            _electivePosictionRepository = electivePositionsRepository;
//            _electivePositionsValidator = electivePositionsValidator;
//        }

//        public async Task<ValidationResult> AlterState(ElectivePosictionsDesactiveOrActive dto)
//        {
//            try
//            {
//                //validate de autorization

//                _errors.Add(ElectivePosictionsError.DataInvalid);
//                if (dto == null) return ValidationResult.Failure(_errors);

//                var elective = await _electivePosictionRepository.GetByIdEntitie(dto.IdElectivePosition);
//                _errors.Add(ElectivePosictionsError.NonExistentElectivePosition);
//                if(elective  == null) return ValidationResult.Failure(_errors);
                
//                var electiveEn = new ElectivePositions { 
//                    Id = dto.IdElectivePosition, 
//                    Name = elective.Name,
//                    Description = elective.Description,
//                    State = dto.state  
//                };

//                var validate = await _electivePositionsValidator.ValidateElectivePositions(electiveEn);
//                if (!validate.IsValid) return validate;

//                var alterState = await _electivePosictionRepository.DesactiveEntitie(electiveEn.Id, electiveEn.State);
//                _errors.Add(new Error("Ha ocurrido un error", "Ha ocurrido un error inesperado en la alteración de lso registros. "));
//                if (alterState) return ValidationResult.Failure(_errors);

//                return ValidationResult.Success();
//            }
//            catch(Exception ex)
//            {
//                _errors.Add(new Error("Ha ocurrido un error ", ex.Message));
//                return ValidationResult.Failure(_errors);

//            }
//        }

//        public async Task<ValidationResult> CreateAsync(ElectivePosictionsDto dto)
//        {
//            try
//            {

//                _errors.Add(ElectivePosictionsError.DataInvalid);
//                if(dto == null) return ValidationResult.Failure(_errors);

//                var electiveP = new ElectivePositions()
//                {
//                    Name = dto.Name.Trim().ToLower(),
//                    State = dto.State,
//                    CreateAt = DateTimeOffset.Now,
//                    CreateUserId = 0, //modificar para que este valor venga de la cookie
//                    Description = dto.Descriptions,
//                };

//                //agregar aqui llamada del elemento de  autorization

//                var validate  = await _electivePositionsValidator.ValidateElectivePositions(electiveP);
//                if (!validate.IsValid) return validate;

//                var create = await _electivePosictionRepository.CreateEntiteAsync(electiveP);

//                if(!create)
//                {
//                    _errors.Add(new Error("Ha ocurrido un error inesperado", "Ha ocurrido un fallo a interntar crear la posición electiva, intente lo de nuevo más tarde."));
//                    return ValidationResult.Failure(_errors);
//                }

//                return ValidationResult.Success();

//            }catch(Exception ex)
//            {
//                _errors.Add(new Error("Error en la creación de la posición electiva", ex.Message));
//                return ValidationResult.Failure(_errors);
//            }
//        }

//        public async Task<IReadOnlyCollection<ElectivePosictionsDto>> GetAllActiveElectivePosictionsAsync()
//        {
//            try
//            {
//                //agregar validaciones de autorization  
//                var electivesPosictions = await _electivePosictionRepository.GetAllActiveAsync();
//                if(electivesPosictions == null)
//                {
//                    _errors.Add(new Error("Ha ocurrido un error inesperado", "Ha ocurrido un fallo al intentar  obtener las posiciones electivas activas, favor intente más tarde."));
//                    return [];
//                }

//                var electiveListDto = new List<ElectivePosictionsDto>();
//                foreach(var itme in  electivesPosictions)
//                {
//                    var dto = new ElectivePosictionsDto()
//                    {
//                        Id  = itme.Id,
//                        Name = itme.Name,
//                        State = itme.State,
//                        Descriptions = itme.Description
//                    };
//                    electiveListDto.Add(dto);
//                }

//                return electiveListDto;

//            }catch(Exception)
//            {
//                return new List<ElectivePosictionsDto>();
//            }
//        }

//        public async Task<IReadOnlyCollection<ElectivePosictionsDto>> GetAllAsync()
//        {
//            try
//            {
//                //agregar validaciones de autorization
//                //

//                var electivesPosictionsDto = await _electivePosictionRepository.GetAllAsync();
//                if (electivesPosictionsDto == null) return [];

//                var listElectivesPosictionsDto = new List<ElectivePosictionsDto>();
//                foreach(var item in electivesPosictionsDto)
//                {
//                    var elective = new ElectivePosictionsDto()
//                    {
//                        Id = item.Id,
//                        Name =item.Name,
//                        State = item.State,
//                        Descriptions = item.Description
//                    };
//                    listElectivesPosictionsDto.Add(elective);
//                }
//                return listElectivesPosictionsDto;
//            }
//            catch (Exception)
//            {

//                return new List<ElectivePosictionsDto>();
//            }
//        }

//        public async Task<ValidationResult<ElectivePosictionsDto>> GetAllById(int Id)
//        {
//            try
//            {
//                //agregar validaciones de autorization 

//                var elective = await _electivePosictionRepository.GetByIdEntitie(Id);
//                _errors.Add(new Error("Ha ocurrido un error inesperado", "Ha ocurrido un error al intentar obtener los datos de este registro."));
//                if (elective == null) return ValidationResult<ElectivePosictionsDto>.Failure(_errors);

//                var dto = new ElectivePosictionsDto() { 
//                    Id = Id,
//                    Name = elective.Name,
//                    Descriptions = elective.Description,
//                    State = elective.State
//                };

//                return ValidationResult<ElectivePosictionsDto>.Success(dto);
//            }
//            catch (Exception ex) {
//                _errors.Add(new Error(""))
//                return ValidationResult<ElectivePosictionsDto>.Failure();
//            }
                   
//        }

//        public async Task<IReadOnlyCollection<ElectivePosictionsDto>> GetElectivePosictionsByDate(DateTimeOffset dateStart, DateTimeOffset dateEnd)
//        {
//            try
//            {
//                var electivesP = await _electivePosictionRepository.GetAllDateAsync(dateStart, dateEnd);
//                if (electivesP == null) return [];

//                var electivesPDto = new List<ElectivePosictionsDto>();
//                foreach(var election in electivesP)
//                {
//                    var dto = new ElectivePosictionsDto() { 
//                        Id = election.Id,
//                        Name = election.Name,
//                        Descriptions = election.Description,
//                        State = election.State
//                    };

//                    electivesPDto.Add(dto);
//                }
//                return electivesPDto;

//            } catch (Exception) { return new List<ElectivePosictionsDto>(); }
//        }

//        public async Task<ValidationResult> UpdateAsync(ElectivePosictionsDto dto)
//        {
//            try
//            {
//                //agregar validaciones de autorizacion

//                var eleccion = new ElectivePositions() {
//                    Id =dto.Id,
//                    Name = dto.Name,
//                    Description = dto.Descriptions,
//                    State = dto.State,
//                    UpdateUserId = 0,//agregar desde el usuario de la cookie que esta haciendo la operacion
//                    UpdateAt = DateTimeOffset.Now
//                };
                
//                var validate =  await _electivePositionsValidator.ValidateElectivePositions(eleccion);
//                if (!validate.IsValid) return validate;

//                var update = await _electivePosictionRepository.UpdateEntitieAsync(eleccion);
//                _errors.Add(new Error("Error inesperado", "Ha ocurrido un error inesperado en la modificación del registro, favor intente de nuevo mas tarde."));
//                if (!update) return ValidationResult.Failure(_errors);

//                return ValidationResult.Success();

//            }catch(Exception ex)
//            {
//                _errors.Add(new Error("Ha ocurrido un error en la comunicación", ex.Message));
//                return ValidationResult.Failure(_errors);
//            }
//        }
//    }
//}
