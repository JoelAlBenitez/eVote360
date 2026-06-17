using Microsoft.AspNetCore.Mvc;
using eVote360.Core.Application.Contracts.Citizens.Command;
using eVote360.Core.Application.Contracts.Citizens.Query;
using eVote360.Core.Application.ViewModels.Citizens;
using eVote360.Core.Application.DTOs.Citizens;
using Microsoft.AspNetCore.Authorization;


namespace eVote360.Presentation.EVote360.Controllers.Citizens
{
    [Authorize(Roles = "Admin")]
    public class CitizenController : Controller
    {

        private readonly ICitizensAlterStateCommand _citizensAlterStateCommand;
        private readonly ICitizensCreateCommand _citizensCreateCommand;
        private readonly ICitizensEditCommand _citizensEditCommand;

        private readonly ICitizensGetActiveQuery _citizensGetActiveQuery;
        private readonly ICitizensGetAllQuery _citizensGetAllQuery;
        private readonly ICitizensGetByIdQuery _citizensGetByIdQuery;
        
        public CitizenController(ICitizensAlterStateCommand citizensAlterStateCommand, 
            ICitizensCreateCommand citizensCreateCommand, 
            ICitizensEditCommand citizensEditCommand
            , ICitizensGetActiveQuery citizensGetActiveQuery,
            ICitizensGetAllQuery citizensGetAllQuery,
            ICitizensGetByIdQuery citizensGetByIdQuery)
        {
            _citizensAlterStateCommand = citizensAlterStateCommand;
            _citizensCreateCommand = citizensCreateCommand;
            _citizensEditCommand = citizensEditCommand;
            _citizensGetActiveQuery = citizensGetActiveQuery;
            _citizensGetAllQuery = citizensGetAllQuery;
            _citizensGetByIdQuery = citizensGetByIdQuery;
        }

        
        public async Task<IActionResult> Index()
        {
            var citizens = await _citizensGetAllQuery.GetAllAsync();
            var views = new List<CitizensViewModel>();

            foreach(var item in citizens)
            {
                var view = new CitizensViewModel { 
                    Id = item.Id,
                    Identification = item.Identification,
                    Name = item.Name,
                    LastName = item.LastName,
                    State = item.State,
                    Email = item.Email,
                    
                };
                views.Add(view);

            }
            return View(views);
        }

        [HttpGet]
        public async Task<IActionResult> GetActive()
        {
            var citizens = await _citizensGetActiveQuery.GetActiveAsync();
            var views = new List<CitizensViewModel>();

            foreach (var item in citizens)
            {
                var view = new CitizensViewModel
                {
                    Id = item.Id,
                    Identification = item.Identification,
                    Name = item.Name,
                    LastName = item.LastName,
                    State = item.State,
                    Email = item.Email,

                };
                views.Add(view);

            }
            return View("Index", views);
        }

        public async Task<IActionResult> AlterState(Guid Id)
        {
            var citizen = await _citizensGetByIdQuery.GetByIdAsync(Id);

            if (!citizen.IsValid)
            {
               foreach (var item in citizen.errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return RedirectToAction(nameof(Index));
            }

            return View("AlterState", new CitizensViewModelAlterState { Id = citizen.Value!.Id, Identification = citizen.Value!.Identification});
        }

        public async Task<IActionResult> Edit(Guid Id)
        {

            var citizen = await _citizensGetByIdQuery.GetByIdAsync (Id);
            if (!citizen.IsValid)
            {
                foreach(var item in citizen.errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return RedirectToAction(nameof(Index));
            }

            return View("Edit", new CitizensViewModelEdit { 
                Id = citizen.Value!.Id,
                Identification = citizen.Value!.Identification,
                Email = citizen.Value!.Email,
                Name = citizen.Value!.Name,
                LastName = citizen.Value!.LastName,
                State = citizen.Value!.State,

            });
        }

        public  Task<IActionResult> Create()
        {
            return Task.FromResult<IActionResult>(View("Save", new CitizensViewModelCreate
            {
                Identification = "",
                Name = "",
                LastName = "",
                Email = "",
                State = true

            }));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CitizensViewModelCreate citizens)
        {
            if (!ModelState.IsValid)
                return View("Save", citizens);

            var dto = new CitizensDto
            {
                Identification = citizens.Identification,
                Name = citizens.Name,
                LastName = citizens.LastName,
                Email = citizens.Email,
                State = citizens.State,
            };

            var create = await _citizensCreateCommand.CreateAsync(dto);
            if (!create.IsValid)
            {
                foreach (var item in create.errors)
                    ModelState.AddModelError(item.Code, item.Description);
                return View("Save", citizens);
            }

            TempData["Message"] = "Ciudadano registrado con éxito";
            TempData["TypeAlert"] = "success";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CitizensViewModelEdit citizens)
        {
            if (!ModelState.IsValid)
                return View("Edit", citizens);

            var dto = new CitizensDto
            {
                Id = citizens.Id,
                Identification = citizens.Identification,
                Name = citizens.Name,
                LastName = citizens.LastName,
                Email = citizens.Email,
                State = citizens.State,
            };

            var edit = await _citizensEditCommand.UpdateAsync(dto);
            if (!edit.IsValid)
            {
                foreach (var item in edit.errors)
                    ModelState.AddModelError(item.Code, item.Description);
                return View("Edit", citizens);
            }

            TempData["Message"] = "Ciudadano editado con éxito.";
            TempData["TypeAlert"] = "success";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AlterState(CitizensDesactiveOrActive citizens)
        {
            if (!ModelState.IsValid) {

                ModelState.AddModelError("", "Ha ocurrido un error en el procesamiento de la solicitud por lo que el formulario no puede ser cargado.");
                return RedirectToAction(nameof(Index));

            }
            var alter = await _citizensAlterStateCommand.AlterStateAsync(citizens.Id);
            if (!alter.IsValid)
            {
                foreach (var item in alter.errors) {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));

        }
    }

}
