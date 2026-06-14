using eVote360.Core.Application.Contracts.Authentication.Command;
using eVote360.Core.Application.Contracts.Authentication.Query;
using eVote360.Core.Application.DTOs.Login;
using eVote360.Core.Application.ViewModels.Login;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace eVote360.Presentation.EVote360.Controllers.Account
{
    public class AccountController : Controller
    {
  
        private readonly ILoginQuery _loginQuery;

        public AccountController(
            ILoginQuery loginQuery)
        {
         
            _loginQuery = loginQuery;
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel login)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error campos no validos favor contemplar todos los campos pertinentes");
                return View(login);
            }

            var dto = new LoginDto { userName = login.UserName, password = login.Password };
            var user = await _loginQuery.UserLoginCommand(dto);

            if (!user.IsValid)
            {
                foreach (var item in user.errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return RedirectToAction(nameof(LogIn));
            }

            var newDto = new LoginDto { 
                IdUser = user.Value!.IdUser,
                userName = user.Value!.userName,
                password = "",
                Role = user.Value!.Role
            };

            var claims = new List<Claim> {

                new Claim(ClaimTypes.NameIdentifier, newDto.IdUser.ToString()),
                new Claim(ClaimTypes.Name, newDto.userName),
                new Claim(ClaimTypes.Role, newDto.Role.ToString())

            };
            var claimIdentity = new ClaimsIdentity(claims, "CookieAuth");
            var claimsFirts = new ClaimsPrincipal(claimIdentity);

            await HttpContext.SignInAsync("CookieAuth", claimsFirts);

            if ((int)user.Value.Role == 1)
                return RedirectToAction("Index", "AdminHome");

            if ((int)user.Value.Role == 2)
                return RedirectToAction("Index", "LeaderHome");

           TempData["Message"] = "Error el rol del usuario no fue encontrado, favor intente de nuevo o contacte con un administrador";
           TempData["TypeAlert"] = "danger";
           return RedirectToAction(nameof(login));
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction(nameof(LogIn));
        }
      
    }
}
