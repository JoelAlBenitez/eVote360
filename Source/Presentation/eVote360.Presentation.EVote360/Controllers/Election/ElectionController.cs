using eVote360.Core.Application.Contracts.Election.Commands;
using eVote360.Core.Application.Contracts.Election.Query;
using eVote360.Core.Application.DTOs.Election;
using eVote360.Core.Application.ViewModels.Election;
using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.Settings.ValueObjects.ElectionDate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
     
      namespace eVote360.Presentation.EVote360.Controllers.Election
      {
    [Authorize(Roles = "Admin")]

          public class ElectionController : Controller
         {
             private readonly IElectionCreateCommand _createCommand;
             private readonly IElectionUpdateCommand _updateCommand;
             private readonly IElectionAlterStateCommand _alterStateCommand;
    
             private readonly IElectionGetAllQuery _getAllQuery;
             private readonly IElectionGetByIdQuery _getByIdQuery;
             private readonly IElectionByYearQuery _getByYearQuery;
             private readonly IGetElectionReportQuery _reportQuery;
    
             public ElectionController(
                 IElectionCreateCommand createCommand,
                 IElectionUpdateCommand updateCommand,
                 IElectionAlterStateCommand alterStateCommand,
                 IElectionGetAllQuery getAllQuery,
                 IElectionGetByIdQuery getByIdQuery,
                 IElectionByYearQuery getByYearQuery,
                 IGetElectionReportQuery reportQuery)
             {
                 _createCommand = createCommand;
                 _updateCommand = updateCommand;
                 _alterStateCommand = alterStateCommand;
                 _getAllQuery = getAllQuery;
                 _getByIdQuery = getByIdQuery;
                 _getByYearQuery = getByYearQuery;
                 _reportQuery = reportQuery;
             }
             public async Task<IActionResult> Index()
             {
                 var elections = await _getAllQuery.ExecuteAsync();
                 var views = new List<ElectionViewModel>();
    
                 foreach (var item in elections)
                 {
                     views.Add(new ElectionViewModel
                     {
                         Id = item.Id,
                         Name = item.Name,
                         State = item.State,
                         ElectionDate = item.ElectionDate,
                         ElectionState = item.ElectionState
                     });
             }
                 return View(views);
         }

    [HttpGet]
             public async Task<IActionResult> FilterByYear(int year)
             {
                 if (year <= 0) return RedirectToAction(nameof(Index));
    
                 var elections = await _getByYearQuery.ExecuteAsync(year);
                 var views = new List<ElectionViewModel>();
    
                 foreach (var item in elections)
                     {
                         views.Add(new ElectionViewModel
    
                         {
                                 Id = item.Id,
                         Name = item.Name,
                         State = item.State,
                         ElectionDate = item.ElectionDate,
                         ElectionState = item.ElectionState
                         });
                     }
                 return View("Index", views);
             }

             public IActionResult Create()
             {
                return View("Save", new ElectionCreateViewModel());
             }

    [HttpPost]
             public async Task<IActionResult> Create(ElectionCreateViewModel model)
             {
                 if (!ModelState.IsValid) return View("Save", model);
    
                 var dto = new ElectionDto
                 {
                         Name = model.ElectionName!,
                     ElectionDate = model.ElectionDate,
                     State = true
                 }
    ;
    
                var result = await _createCommand.ExecuteAsync(dto);
    
                if (!result.IsValid)
                     {
                         foreach(var error in result.errors) { ModelState.AddModelError(error.Code, error.Description);}
                         return View("Save", model);
                     }
    
                TempData["Message"] = "Elección creada con éxito";
                 return RedirectToAction(nameof(Index));
             }
        [HttpGet]
            public async Task<IActionResult> Edit(int id)
            {
                 var result = await _getByIdQuery.ExecuteAsync(id);
            
                 if (result== null ||result.Value == null)
                 {
                 return RedirectToAction(nameof(Index));
                 }
            
                 var election = result.Value;
            
                 var viewModel = new ElectionEditViewModel
                 {
                 Id = election.Id,
                 Name = election.Name,
                 ElectionDate = election.ElectionDate,
                 ElectionState = election.ElectionState,
                 State = election.State
                 };
            
                 return View("Save", viewModel);
                       }

   [HttpPost]
            public async Task<IActionResult> Edit(ElectionEditViewModel model)
            {
                 if (!ModelState.IsValid) return View("Save", model);
    
                var dto = new ElectionDto
                {
                         Id = model.Id,
                    Name = model.Name,
                    ElectionDate = model.ElectionDate,
                    ElectionState = model.ElectionState,
                    State = model.State
                };
                var result = await _updateCommand.ExecuteAsync(dto);
    
                if (!result.IsValid)
                     {
                foreach (var error in result.errors) { ModelState.AddModelError(error.Code, error.Description); }
                return View("Save", model);
                     }
    
                TempData["Message"] = "Elección actualizada con éxito";
                return RedirectToAction(nameof(Index));
             }

        [HttpPost]
        public async Task<IActionResult> AlterState(int id)
        {
            var result = await _alterStateCommand.ExecuteAsync(id);

            if (!result.IsValid)
            {
                var primerError = result.errors.FirstOrDefault();
                TempData["TypeAlert"] = "error";
                TempData["Message"] = primerError != null ? primerError.Description : "Error al intentar cambiar el estado.";
            }
            else
            {
                TempData["TypeAlert"] = "success";
                TempData["Message"] = "Estado modificado con éxito.";
            }

            return RedirectToAction(nameof(Index));
        }

            public async Task<IActionResult> Results(int id)
            {
    var reportResult = await _reportQuery.ExecuteAsync(id);
    
                if (reportResult == null)
                     {
                         TempData["Message"] = "Aún no hay resultados procesados para esta elección.";
                         return RedirectToAction(nameof(Index));
                     }
    
                return View(reportResult);
             }
     }
    }