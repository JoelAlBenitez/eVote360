using eVote360.Core.Application.Contracts.ElectivePosictions.Commands;
using eVote360.Core.Application.Contracts.ElectivePosictions.Query;
using eVote360.Core.Application.Contracts.ElectivePosictions.QueryServices;
using eVote360.Core.Application.DTOs.ElectivePositions;
using eVote360.Core.Application.ViewModels.ElectivePositions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVote360.Presentation.EVote360.Controllers.ElectivePosictions
{


    [Authorize(Roles = "Admin")]
    public class ElectivePosictionsController : Controller
    {

        private readonly IElectivePosictionsCreateCommand _electivePosictionsCreateCommand;
        private readonly IElectivePosictionsUpdateCommand _electivePosictionsUpdate;
        private readonly IElectivePosictionsGetActiveQuery _electivePosictionsGetActiveQuery;
        private readonly IElectivePosictionsGetAllQuery _electivePosictionsGetAllQuery;
        private readonly IElectivePosictionsGetByIdQuery _electivePosictionsGetByIdQuery;
        private readonly IElectivePosictionsGetElectivesPosictionsByDateQuery _electivePosictionsGetElectivesPosictionsByDateQuery;
        private readonly IElectivePosictionsAlterState _electivePosictionsAlterState;

        public ElectivePosictionsController(
            IElectivePosictionsCreateCommand electivePosictionsCreateCommand,
            IElectivePosictionsUpdateCommand electivePosictionsUpdate,
            IElectivePosictionsGetActiveQuery electivePosictionsGetActiveQuery, 
            IElectivePosictionsGetAllQuery electivePosictionsGetAllQuery,
            IElectivePosictionsGetByIdQuery electivePosictionsGetByIdQuery, 
            IElectivePosictionsGetElectivesPosictionsByDateQuery 
            electivePosictionsGetElectivesPosictionsByDateQuery, 
            IElectivePosictionsAlterState electivePosictionsAlterState
          )
        {
            _electivePosictionsCreateCommand = electivePosictionsCreateCommand;
            _electivePosictionsUpdate = electivePosictionsUpdate;
            _electivePosictionsGetActiveQuery = electivePosictionsGetActiveQuery;
            _electivePosictionsGetAllQuery = electivePosictionsGetAllQuery;
            _electivePosictionsGetByIdQuery = electivePosictionsGetByIdQuery;
            _electivePosictionsGetElectivesPosictionsByDateQuery = electivePosictionsGetElectivesPosictionsByDateQuery;
            _electivePosictionsAlterState = electivePosictionsAlterState;
        }

        public async Task<IActionResult> Index()
        {
            var electiveP = await _electivePosictionsGetAllQuery.GetAllAsync();
            var electivePViewModel = new List<ElectivePosictionsViewModel>();
            
            foreach( var item in electiveP)
            {
                var elective = new ElectivePosictionsViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Descriptions,
                    State = item.State
                };

                electivePViewModel.Add(elective);
            }
            return View(electivePViewModel);
        }

        [HttpGet]
        public async  Task<IActionResult> GetElectivePosictionByActive()
        {

            var electiveP = await  _electivePosictionsGetActiveQuery.GetAllActiveElectivePosictionsAsync();
            var electivePViewModel = new List<ElectivePosictionsViewModel>();

            foreach (var item in electiveP)
            {
                var elective = new ElectivePosictionsViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Descriptions,
                    State = item.State
                };

                electivePViewModel.Add(elective);
            }
            return View("Index", electivePViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetElectiveByDate(FilterElectivePosictionsByDateViewModel vp)
        {
            var electiveP = await  _electivePosictionsGetElectivesPosictionsByDateQuery.GetElectivePosictionsByDate(vp.From, vp.To);
            var electivePViewModel = new List<ElectivePosictionsViewModel>();
            foreach (var item in electiveP)
            {
                var elective = new ElectivePosictionsViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Descriptions,
                    State = item.State
                };

                electivePViewModel.Add(elective);
            }
            return View("Index", electivePViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetElectivePosiction(int Id)
        {
            var elective = await _electivePosictionsGetByIdQuery.GetAllById(Id);
          
                if (!elective.IsValid)
                {
                    foreach (var item in elective.errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }
                    return RedirectToAction(nameof(Index));
                }
            

            var vm = new ElectivePosictionsViewModel { 
                Id = elective.Value!.Id,
                Name = elective.Value.Name,
                Description = elective.Value.Descriptions,
                State = elective.Value.State
            };

            return PartialView("_ViewElectivPosiction", vm);
        }

        public Task <IActionResult> Create()
        {
            return Task.FromResult<IActionResult>(View("Save", new ElectivePosictionsViewModelCreate
            {
                Name = "",
                Description = "",
                State = true
            }));
                    
               
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var elective = await _electivePosictionsGetByIdQuery.GetAllById(Id);
            if (!elective.IsValid) {
                foreach (var item in elective.errors) {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return RedirectToAction(nameof(Index));
            }
            var electiveVm = new ElectivePosictionsViewModelEdit { 
                Id = elective.Value!.Id,
                Name = elective.Value.Name,
                Description = elective.Value.Descriptions,
                State = elective.Value.State
            };
            return View("Edit", electiveVm);
        }

        public async Task<IActionResult> AlterState(int Id)
        {
            var elective = await _electivePosictionsGetByIdQuery.GetAllById(Id);
            if (!elective.IsValid)
            {
                foreach (var item in elective.errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return RedirectToAction(nameof(Index));
            }
            var electiveVm = new ElectivePosictionsViewModelDelete {
                
                Id = elective.Value!.Id,
                Name = elective.Value.Name
       
            };
            return View("AlterState", electiveVm);

        }

        [HttpPost]
        public async Task<IActionResult> Create(ElectivePosictionsViewModelCreate electiveVM)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error en la creación y carga del formulario ");
                return RedirectToAction(nameof(Create));
            }

            var dto = new ElectivePosictionsDto
            {
                Name = electiveVM.Name,
                Descriptions = electiveVM.Description,
                State = electiveVM.State
            };
            var create = await _electivePosictionsCreateCommand.CreateAsync(dto);
          
                if (!create.IsValid)
                {
                    foreach (var item in create.errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }
                    return RedirectToAction(nameof(Create));
               }
            

            TempData["Message"] = "Posición electiva creada exitosamente";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ElectivePosictionsViewModelEdit electiveVM)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error en la creación y carga del formulario ");
                return RedirectToAction(nameof(Edit));
            }
            var dto = new ElectivePosictionsDto
            {
                Name = electiveVM.Name,
                Descriptions = electiveVM.Description,
                State = electiveVM.State
            };

            var edit = await _electivePosictionsUpdate.UpdateAsync(dto);
            if (!edit.IsValid)
                {
                    foreach (var item in edit.errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }
                    return RedirectToAction(nameof(Edit));
                }

            
            TempData["Message"] = "Posición electiva editada exitosamente";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AlterState(ElectivePosictionsDesactiveOrActive electiveVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error en la selección del registro y carga del elemento");
                return RedirectToAction(nameof(Index));
            }

            var dto = new ElectivePosictionsDesactiveOrActive(electiveVM.IdElectivePosition);
            var alter = await _electivePosictionsAlterState.AlterState(dto);
          
                if (!alter.IsValid)
                { 
                    foreach (var item in alter.errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }
                    return RedirectToAction(nameof(Index));
                }
               
            

            
            TempData["Message"] = "Se modifico el estado de la posición exitosamente";
            return RedirectToAction(nameof(Index));
        }


    }
}
