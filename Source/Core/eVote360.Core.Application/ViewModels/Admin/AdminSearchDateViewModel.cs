using System.ComponentModel.DataAnnotations;

namespace eVote360.Core.Application.ViewModels.Admin
{
    public sealed class AdminSearchDateViewModel
    {
        [Required(ErrorMessage = "El año de la elección es requerido")]
        public required int Year { get; set; }
        public required List<int> YearAvaible { get; set; }
    }
}
