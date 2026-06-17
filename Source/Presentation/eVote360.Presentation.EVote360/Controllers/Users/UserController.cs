using eVote360.Core.Application.Contracts.Users.Commands;
using eVote360.Core.Application.Contracts.Users.Query;
using eVote360.Core.Application.DTOs.Users;
using eVote360.Core.Application.ViewModels.Users;
using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.Settings.ValueObjects.UserPassword;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
    
     namespace eVote360.Presentation.EVote360.Controllers.Users
     {
        [Authorize(Roles = "Admin")]
        public class UserController : Controller
        {
            private readonly IUserCreateCommand _createCommand;
            private readonly IUserEditCommand _editCommand;
            private readonly IUserAlterStateCommand _stateCommand;
            private readonly IUserGetAllQuery _getAllQuery;
            private readonly IUserGetByIdQuery _getByIdQuery;
   
            public UserController(
                IUserCreateCommand createCommand,
                IUserEditCommand editCommand,
                IUserAlterStateCommand stateCommand,
                IUserGetAllQuery getAllQuery,
                IUserGetByIdQuery getByIdQuery)
            {
                _createCommand = createCommand;
                _editCommand = editCommand;
                _stateCommand = stateCommand;
                _getAllQuery = getAllQuery;
                _getByIdQuery = getByIdQuery;
            }

            public async Task<IActionResult> Index()
     {
              var users = await _getAllQuery.ExecuteAsync(); 
    
              var viewModels = users.Select(item => new UsersViewModel
              {
                 Id = item.Id,
                 UserFirstName = item.UserFirstName,
                 UserLastName = item.UserLastName,
                 UserEmail = item.UserEmail,
                 UserRole = item.UserRole,
                 State = item.State,
                 Name = item.Name
                 }).ToList();
   
               return View(viewModels);
            }

        public IActionResult Create()
        {
            return View("Save", new UsersViewModelCreate
            {
                UserFirstName = "",
                UserLastName = "",
                UserEmail = "",
                UserPassword = "",
                Name = "",
            });
        }

    [HttpPost]
     public async Task<IActionResult> Create(UsersViewModelCreate vm)
     {
         if (!ModelState.IsValid) return View("Save", vm);
   
        var dto = new UsersDto
       {
                 UserFirstName = vm.UserFirstName,
            UserLastName = vm.UserLastName,
            UserEmail = vm.UserEmail,
            UserPassword = vm.UserPassword,
            UserRole = vm.UserRole,
            Name = vm.Name,
            State = true,
   
            CreateAt = null,
            CreateUserId = null,
            UpdateAt = null,
            UpdateUserId = null
        }
    ;
    
        var result = await _createCommand.ExecuteAsync(dto);
    
        if (!result.IsValid)
             {
                 foreach (var error in result.errors) ModelState.AddModelError(error.Code, error.Description);
                 return View("Save", vm);
             }
    
        TempData["Message"] = "Usuario registrado con éxito";
         return RedirectToAction(nameof(Index));
     }

     public async Task<IActionResult> Edit(int id)
     {
         var result = await _getByIdQuery.ExecuteAsync(id);
    
         if (result == null || !result.IsValid) return RedirectToAction(nameof(Index));
    
         var vm = new UsersViewModelEdit
         {
            Id = id,
            UserFirstName = result.Value!.UserFirstName,
            UserLastName = result.Value!.UserLastName,
            UserEmail = result.Value!.UserEmail,
            UserRole = result.Value!.UserRole,
            Name = result.Value!.Name,
            State = result.Value!.State,
            UserPassword = ""
        }
    ;
    
        return View("Save", vm);
     }

   [HttpPost]
    public async Task<IActionResult> Edit(UsersViewModelEdit vm)
    {
        if (!ModelState.IsValid) return View("Save", vm);

        var dto = new UsersDto
        {
            Id = vm.Id,
            UserFirstName = vm.UserFirstName,
            UserLastName = vm.UserLastName,
            UserEmail = vm.UserEmail,
            UserPassword = string.IsNullOrWhiteSpace(vm.UserPassword) ? "" : vm.UserPassword,
            UserRole = vm.UserRole,
            Name = vm.Name,
            State = vm.State,

            CreateAt = null,
            CreateUserId = null,
            UpdateAt = null,
            UpdateUserId = null
        };
    
        var result = await _editCommand.ExecuteAsync(dto);
    
        if (!result.IsValid)
             {
                 foreach (var error in result.errors) ModelState.AddModelError(error.Code, error.Description);
                 return View("Save", vm);
             }
    
        TempData["Message"] = "Usuario actualizado con éxito";
         return RedirectToAction(nameof(Index));
     }

        [HttpPost]
        public async Task<IActionResult> AlterState(int id, bool state)
        {
            var result = await _stateCommand.ExecuteAsync(id, !state);
            if (!result.IsValid) {

                var firstError = result.errors.FirstOrDefault();
                TempData["TypeAlert"] = "danger";

                TempData["Message"] = firstError != null ? firstError.Description : "Error al intentar cambiar el estado";
            }

            else
            {
                TempData["TypeAlert"] = "success";
                TempData["Message"] = "Estado del usuario modificado con éxito.";
            }

            return RedirectToAction(nameof(Index));
 }
        }
     }