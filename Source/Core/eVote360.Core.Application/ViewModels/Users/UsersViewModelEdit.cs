using eVote360.Core.Application.ViewModels.Base;
using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.Settings.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Application.ViewModels.Users
{
    public sealed class UsersViewModelEdit 
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "Se requiere un Id valido")]
        public required int Id { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Se requiere un username valido")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Ingrese un username mayor a 5 caracteres y no mayor a 30")]
        public required string Name { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Se requiere un nombre valido")]
        public required bool State { get; set; }

        [Display(Name = "Primer Nombre")]
        [Required(ErrorMessage = "Se requiere un nombre valido")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Ingrese un nombre mayor a 5 caracteres y no mayor a 30")]
        public required string UserFirstName { get; set; }



        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "Se requiere un apellido valido")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Ingrese un apellido mayor a 5 caracteres y no mayor a 30")]
        public required string UserLastName { get; set; }

        [Display(Name = "Correo")]
        [Required(ErrorMessage = "Se requiere un correo valido")]
        public required string UserEmail { get; set; }

        [Display(Name = "Contraseña")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Ingrese una contraseña mayor a 5 caracteres y no mayor a 30")]
        public string? UserPassword { get; set; }

        [Display(Name = "Rol de Usuario")]
        [Required(ErrorMessage = "Se requiere un rol de usuario valido")]
        public UserRole UserRole { get; set; }
    }
}
