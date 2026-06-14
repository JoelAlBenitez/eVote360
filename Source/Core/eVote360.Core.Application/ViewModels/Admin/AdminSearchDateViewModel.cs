using System.ComponentModel.DataAnnotations;

namespace eVote360.Core.Application.ViewModels.Admin
{
    public sealed class AdminSearchDateViewModel
    {
        [Required(ErrorMessage = "El año de la eleccion es requerido")]
        public required int Year { get; set; }
    }
}
