using System.ComponentModel.DataAnnotations;

namespace eVote360.Core.Application.Services.Elector.Code
{
    public sealed class CodeVerificationViewModel
    {

        [Display(Name = "Codigo de verificacion")]
        [Required(ErrorMessage = " Ingrese un codigo de verificacion valido, el codigo es requerido")]
        public required int CodeVerification {  get; set; }
    }
}
