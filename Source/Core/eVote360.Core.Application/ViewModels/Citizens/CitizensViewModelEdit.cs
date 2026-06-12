
using System.ComponentModel.DataAnnotations;

namespace eVote360.Core.Application.ViewModels.Citizens
{
    public sealed class CitizensViewModelEdit
    {

        public required Guid Id { get; set; }

        [Display(Name = "Identificación")]
        [Required(ErrorMessage = "El número de identificación es requerido")]
        [StringLength(11, ErrorMessage = "El número de identificación debe tener exactamente 11 digitos", MinimumLength = 11)]
        public required string Identification { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre del ciudadano es requerido")]
        [StringLength(40, ErrorMessage = "El nombre del ciudadano no puede tener una longitud mayor a 30 caracteres.", MinimumLength = 1)]
        public required string Name { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El apellido del ciudadano es requerido")]
        [StringLength(40, ErrorMessage = "El apellido del ciudadano no puede tener una longitud mayor a 30 caracteres.", MinimumLength = 1)]
        public required string LastName { get; set; }


        [Display(Name = "Correo Electrónico")]
        [Required(ErrorMessage = "El correo electrónico es requerido.")]
        [StringLength(254, ErrorMessage = "El correo no puede tener una catidad mayor a 254 caracteres.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un formato de correo electrónico valido.")]
        public required string Email { get; set; }


        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El estado del ciudadano es requerido")]
        public required bool State { get; set; }

    }
}
