using System.ComponentModel.DataAnnotations;

namespace eVote360.Core.Application.ViewModels.Login
{
    public sealed class LoginViewModel
    {
        [Required(ErrorMessage = "EL nombre del usuario es requerido para autenticarse")]
        [Display(Name = "Nombre de usuario")]
        [StringLength(20, ErrorMessage = "El nombre de usuario ingresado no es valido, el mismo supera la cantidad maxima de caracteres permitidas.")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "La contraseña del usuario es requerida para autenticarse")]
        [Display(Name = "Contraseña")]
        [StringLength(20, ErrorMessage = "La contraseña del usuario ingresada no es valida, la misma supera la cantidad maxima de caracteres permitidos.")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
