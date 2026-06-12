using eVote360.Core.Domain.Commom.BaseEntity;
using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace eVote360.Core.Application.ViewModels.Users
{
    public sealed class UsersViewModelCreate
    {
        [Display(Name = "Nombre")]
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
        [Required(ErrorMessage = "Se requiere una contraseña valida")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Ingrese una contraseña mayor a 5 caracteres y no mayor a 30")]
        public required string UserPassword { get; set; }

        [Display(Name = "Rol de Usuario")]
        [Required(ErrorMessage = "Se requiere un rol de usuario valido")]
        public UserRole UserRole { get; set; }
    }
}
