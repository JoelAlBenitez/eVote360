using eVote360.Core.Application.Contracts.PoliticalParty.Commands;
using eVote360.Core.Application.Contracts.PoliticalParty.Query;
using eVote360.Core.Application.Contracts.Services;
using eVote360.Core.Application.DTOs.PoliticalParty;
using eVote360.Core.Application.Services.PoliticalParty.CommandHandler;
using eVote360.Core.Application.ViewModels.PoliticalParty;
using eVote360.Core.Domain.Settings.ValueObjects.PoliticalPartyAcronym;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
    
    namespace eVote360.Presentation.EVote360.Controllers.PoliticalParty
    {
        
        [Authorize(Roles = "Admin")]
        public class PoliticalPartyController : Controller
        {
            private readonly IPoliticalPartyCreateCommand _createCommand;
            private readonly IPoliticalPartyUpdateCommand _updateCommand;
            private readonly IPoliticalPartyStateCommand _stateCommand;
            private readonly IPoliticalPartyGetAllQuery _getAllQuery;
            private readonly IPoliticalPartyGetByIdQuery _getByIdQuery;
            private readonly IFileStorageService _fileService;
   
            public PoliticalPartyController(
                IPoliticalPartyCreateCommand createCommand,
                IPoliticalPartyUpdateCommand updateCommand,
                IPoliticalPartyStateCommand stateCommand,
                IPoliticalPartyGetAllQuery getAllQuery,
                IPoliticalPartyGetByIdQuery getByIdQuery,
                IFileStorageService fileService)
            {
                _createCommand = createCommand;
                _updateCommand = updateCommand;
                _stateCommand = stateCommand;
                _getAllQuery = getAllQuery;
                _getByIdQuery = getByIdQuery;
                _fileService = fileService;
            }
            public async Task<IActionResult> Index()
            {
                var parties = await _getAllQuery.ExecuteAsync();
   
                
                var viewModels = parties.Select(item => new PoliticalPartyViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    PoliticalPartyDescription = item.PoliticalPartyDescription,
                    PoliticalPartyAcronym = item.PoliticalPartyAcronym,
                    PoliticalPartyLogo = item.PoliticalPartyLogo,
                    State = item.State
                }).ToList();
   
                return View(viewModels);
            }
            public IActionResult Create()
            {
                 return View("Save", new PoliticalPartyViewModelCreate
 
                 {
                         Name = "",
                    PoliticalPartyDescription = "",
                    PoliticalPartyAcronym = "",
                    PoliticalPartyState = true,
                    LogoFile = null 
                });
             }

   [HttpPost]
            public async Task<IActionResult> Create(PoliticalPartyViewModelCreate vm)
            {
                 if (!ModelState.IsValid) return View("Save", vm);
    
                string logoPath = "/images/default-logo.png";
                 if (vm.LogoFile != null)
                     {
                         logoPath = await _fileService.SaveFileAsync(vm.LogoFile, "Parties");
                     }
    
                var dto = new PoliticalPartyDto
                {
                         Name = vm.Name,
                    PoliticalPartyDescription = vm.PoliticalPartyDescription,
                    PoliticalPartyAcronym = vm.PoliticalPartyAcronym,
                    PoliticalPartyLogo = logoPath,
                    State = vm.PoliticalPartyState,

                    CreateAt = null,
                    CreateUserId = null,
                    UpdateUserId = null
                }
    ;
    
                var result = await _createCommand.ExecuteAsync(dto);
   
                if (!result.IsValid)
                     {
                         foreach (var error in result.errors) ModelState.AddModelError(error.Code, error.Description);
                         return View("Save", vm);
                     }
    
                TempData["Message"] = "Partido Político creado con éxito";
                 return RedirectToAction(nameof(Index));
        }
        
     public async Task<IActionResult> Edit(int id)
     {
         var result = await _getByIdQuery.ExecuteAsync(id);
    
         if (result == null)
         {
                ModelState.AddModelError("", "");
                return RedirectToAction(nameof(Index));
        }
   
        var vm = new PoliticalPartyViewModelEdit
        {
            Id = result.Id,
            Name = result.Name,
            PoliticalPartyDescription = result.PoliticalPartyDescription,
            PoliticalPartyAcronym = result.PoliticalPartyAcronym,
            PoliticalPartyLogo = result.PoliticalPartyLogo,
            State = result.State,
            LogoFile = null 
        };

        return View("Save", vm);
 }
   
   [HttpPost]
    public async Task<IActionResult> Edit(PoliticalPartyViewModelEdit vm)
    {
        if (!ModelState.IsValid) return View("Edit", vm);
   
   
        string logoPath = vm.PoliticalPartyLogo;
         if (vm.LogoFile != null)
             {
                 logoPath = await _fileService.SaveFileAsync(vm.LogoFile, "Parties");
             }
    
        var dto = new PoliticalPartyDto
        {
                 Id = vm.Id,
            Name = vm.Name,
            PoliticalPartyDescription = vm.PoliticalPartyDescription,
            PoliticalPartyAcronym = vm.PoliticalPartyAcronym,
            PoliticalPartyLogo = logoPath,
            State = vm.State,
   
            CreateAt = null,
            CreateUserId = null,
            UpdateUserId = null
        }
    ;
    
        var result = await _updateCommand.ExecuteAsync(dto);
   
        if (!result.IsValid)
             {
                 foreach (var error in result.errors) ModelState.AddModelError(error.Code, error.Description);
                 return View("Save", vm);
             }
    
        TempData["Message"] = "Partido Político actualizado con éxito";
         return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> AlterState(int id)
        {
         var result = await _stateCommand.ExecuteAsync(id);

        if (!result.IsValid)
        {
         foreach (var error in result.errors) ModelState.AddModelError(error.Code, error.Description);
         TempData["TypeAlert"] = "error";
         TempData["Message"] = "Error al intentar cambiar el estado.";
        }
        else
        {
         TempData["TypeAlert"] = "success";
         TempData["Message"] = "Estado del partido modificado con éxito.";
        }
        return RedirectToAction(nameof(Index));
        }
        }
    }