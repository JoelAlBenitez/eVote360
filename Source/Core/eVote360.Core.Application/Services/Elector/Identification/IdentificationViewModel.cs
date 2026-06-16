

using System.ComponentModel.DataAnnotations;

namespace eVote360.Core.Application.Services.Elector.Identification
{
    public sealed class IdentificationViewModel
    {
        [Display(Name = "Identificacion")]
        [Required(ErrorMessage = "Debe ingresar una identificacion valida, la identificacion es requerida")]
        [StringLength(11, ErrorMessage = "Debe ingresar una identificacion de 11 digitos sin guiones", MinimumLength =11)]
        public required string Identification {  get; set; }        
    }
}
