using eVote360.Core.Application.Contracts.PoliticalLeaderAssignment.Commands;
using eVote360.Core.Application.Contracts.PoliticalLeaderAssignment.Query;
using eVote360.Core.Application.Contracts.PoliticalParty.Query;
using eVote360.Core.Application.Contracts.Users.Query;
using eVote360.Core.Application.DTOs.PoliticalLeaderAssignment;
using eVote360.Core.Application.ViewModels.PoliticalLeaderAssignment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace eVote360.Presentation.EVote360.Controllers.PoliticalLeaderAssignment
 {
    [Authorize(Roles = "Admin")]
     public class PoliticalLeaderAssignmentController : Controller
     {
         private readonly ILeaderAssignmentCreateCommand _createCommand;
         private readonly ILeaderAssignmentUpdateCommand _updateCommand;
         private readonly ILeaderAssignmentAlterStateCommand _alterStateCommand;
         private readonly ILeaderAssignmentGetAllQuery _getAllQuery;
         private readonly ILeaderAssignmentGetByIdQuery _getByIdQuery;

         private readonly IPoliticalPartyGetActiveQuery _partyGetActiveQuery;
         private readonly IUserGetAllActivesQuery _userGetActiveQuery; 

         public PoliticalLeaderAssignmentController(
             ILeaderAssignmentCreateCommand createCommand,
             ILeaderAssignmentUpdateCommand updateCommand,
             ILeaderAssignmentAlterStateCommand alterStateCommand,
             ILeaderAssignmentGetAllQuery getAllQuery,
             ILeaderAssignmentGetByIdQuery getByIdQuery,
             IPoliticalPartyGetActiveQuery partyGetActiveQuery,
             IUserGetAllActivesQuery userGetActiveQuery)
         {
             _createCommand = createCommand;
             _updateCommand = updateCommand;
             _alterStateCommand = alterStateCommand;
             _getAllQuery = getAllQuery;
             _getByIdQuery = getByIdQuery;
             _partyGetActiveQuery = partyGetActiveQuery;
             _userGetActiveQuery = userGetActiveQuery;
         }
         public async Task<IActionResult> Index()
         {
             var assignments = await _getAllQuery.ExecuteAsync();
             var views = new List<LeaderAssignmentViewModel>();

             foreach (var item in assignments)
             {
                 views.Add(new LeaderAssignmentViewModel
                 {
                     Id = item.Id,
                     Name = item.Name,
                     State = item.State,
                     PoliticalLeaderId = item.PoliticalLeaderId,
                     PoliticalPartyId = item.PoliticalPartyId,
                     PoliticalAssignmentDate = item.PoliticalAssignmentDate
                 });
             }
             return View(views);
         }
         public async Task<IActionResult> Create()
         {
             await LoadCatalogs();
             return View("Save", new LeaderAssignmentCreateViewModel
             {
                 PoliticalAssignmentDate = DateTime.Now,
                 PoliticalLeaderId = 0,
                 PoliticalPartyId = 0
             });
         }


[HttpPost]
         public async Task<IActionResult> Create(LeaderAssignmentCreateViewModel model)
         {
             if (!ModelState.IsValid)
             {
                 await LoadCatalogs();
                 return View("Save", model);
             }

             var dto = new LeaderAssignmentDto
             {
                 PoliticalLeaderId = model.PoliticalLeaderId,
                 PoliticalPartyId = model.PoliticalPartyId,
                 PoliticalAssignmentDate = model.PoliticalAssignmentDate,
                 State = true,
                 Name = "Asignacion",
                 CreateAt = null,
                 CreateUserId = null,
                 UpdateAt = null,
                 UpdateUserId = null,
             }
    ;

             var result = await _createCommand.ExecuteAsync(dto);
             if (!result.IsValid)
             {
                 foreach (var error in result.errors) { ModelState.AddModelError(error.Code, error.Description); }
                 await LoadCatalogs();
                 return View("Save", model);
             }

             TempData["Message"] = "Líder asignado con éxito";
             return RedirectToAction(nameof(Index));
         }


         public async Task<IActionResult> Edit(int id)
         {
             var result = await _getByIdQuery.ExecuteAsync(id);
             if (!result.IsValid) return RedirectToAction(nameof(Index));

             var assignment = result.Value;

             var viewModel = new LeaderAssignmentEditViewModel
             {
                 Id = assignment!.Id,
                 Name = assignment.Name,
                 State = assignment.State,
                 PoliticalLeaderId = assignment.PoliticalLeaderId,
                 PoliticalPartyId = assignment.PoliticalPartyId,
                 PoliticalAssignmentDate = assignment.PoliticalAssignmentDate
             }
    ;

             await LoadCatalogs();
             return View("Save", viewModel);
         }


[HttpPost]
         public async Task<IActionResult> Edit(LeaderAssignmentEditViewModel model)
         {
             if (!ModelState.IsValid)
             {
                 await LoadCatalogs();
                  return View("Save", model);
              }
 
                var dto = new LeaderAssignmentDto
                {
                    Id = model.Id,
                    Name = model.Name,
                    State = model.State,
                    PoliticalLeaderId = model.PoliticalLeaderId,
                    PoliticalPartyId = model.PoliticalPartyId,
                    PoliticalAssignmentDate = model.PoliticalAssignmentDate,
                    CreateAt = null,
                    CreateUserId = null,
                    UpdateAt = null,
                    UpdateUserId = null,
                }
;

             var result = await _updateCommand.ExecuteAsync(dto);

             if (!result.IsValid)
             {
                 foreach (var error in result.errors) { ModelState.AddModelError(error.Code, error.Description); }
                 await LoadCatalogs();
                 return View("Save", model);
             }

             TempData["Message"] = "Asignación actualizada con éxito";
             return RedirectToAction(nameof(Index));
         }


[HttpPost]
        public async Task<IActionResult> AlterState(int id, bool state)
        {
         var result = await _alterStateCommand.ExecuteAsync(id, !state);
    
         if (!result.IsValid)
         {
             var primerError = result.errors.FirstOrDefault();
            TempData["TypeAlert"] = "error";
            TempData["Message"] = primerError != null ? primerError.Description : "Error al intentar cambiar el estado de la asignación.";
         }
         else
         {
            TempData["TypeAlert"] = "success";
            TempData["Message"] = "Estado de la asignación modificado con éxito.";
         }

         return RedirectToAction(nameof(Index));
        }

        private async Task LoadCatalogs()
        {
          ViewBag.Parties = await _partyGetActiveQuery.ExecuteAsync();
          ViewBag.Users = await _userGetActiveQuery.ExecuteAsync();
        }


}
}