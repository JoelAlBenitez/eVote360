using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace eVote360.Core.Application.ViewModels.Elector.Identification
{
    public sealed class IdentifiationFileImagenViewModel
    {
        [Display(Name = "Imagen de identificacion")]
        [Required(ErrorMessage = "Debe subir una imagen, la imagen de verificacion de identidad es requeridad")]

        public required IFormFile IdentificationImg {  get; set; }
    }
}
