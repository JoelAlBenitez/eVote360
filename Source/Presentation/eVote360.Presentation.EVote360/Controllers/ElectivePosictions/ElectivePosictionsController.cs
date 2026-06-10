using eVote360.Core.Application.Contracts.ElectivePosictions.Commands;
using eVote360.Core.Application.Contracts.ElectivePosictions.Query;
using eVote360.Core.Application.Contracts.ElectivePosictions.QueryServices;
using eVote360.Core.Application.DTOs.ElectivePositions;
using eVote360.Core.Application.ViewModels.ElectivePositions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace eVote360.Presentation.EVote360.Controllers.ElectivePosictions
{
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
            return RedirectToAction("Index", electivePViewModel);
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
            return RedirectToAction("Index", electivePViewModel);
        }

        [HttpGet]

        public async Task <IActionResult> Create()
        {
            return View("Save", new ElectivePosictionsViewModelCreate
            {
                Name = "",
                Description = "",
                State = true
            });
                    
               
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
            var dto = new ElectivePosictionsDto
            {
                Name = electiveVM.Name,
                Descriptions = electiveVM.Description,
                State = electiveVM.State
            };
            var create = await _electivePosictionsCreateCommand.CreateAsync(dto);
            if (!create.IsValid)
            {
                if (!create.IsValid)
                {
                    foreach (var item in create.errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }
                    return RedirectToAction(nameof(Create));
                }
            }

            TempData["Message"] = "Posición electiva creada exitosamente";
            TempData["TypeAlert"] = "success";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ElectivePosictionsViewModelEdit electiveVM)
        {
            var dto = new ElectivePosictionsDto
            {
                Name = electiveVM.Name,
                Descriptions = electiveVM.Description,
                State = electiveVM.State
            };

            var edit = await _electivePosictionsUpdate.UpdateAsync(dto);
            if(!edit.IsValid)
            {
                if (!edit.IsValid)
                {
                    foreach (var item in edit.errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }
                    return RedirectToAction(nameof(Edit));
                }

            }
            TempData["Message"] = "Posición electiva editada exitosamente";
            TempData["TypeAlert"] = "success";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AlterState(ElectivePosictionsDesactiveOrActive electiveVM)
        {
            var dto = new ElectivePosictionsDesactiveOrActive(electiveVM.IdElectivePosition);
            var alter = await _electivePosictionsAlterState.AlterState(dto);
            if (!alter.IsValid)
            {
                if (!alter.IsValid)
                { 
                    foreach (var item in alter.errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }
                    return RedirectToAction(nameof(Edit));
                }
                return RedirectToAction(nameof(AlterState));
            }

            TempData["Message"] = "Se modifico el estado de la posición exitosamente";
            TempData["TypeAlert"] = "success";
            return RedirectToAction(nameof(Index));
        }


    }
}
