using System.ComponentModel.DataAnnotations;
namespace eVote360.Core.Application.ViewModels.ElectivePositions
{
    public sealed class ElectivePosictionsViewModelCreate
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Se requiere un nombre valido")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Ingrese un nombre mayor a 5 caracteres y no mayor a 30")]
        public required string Name { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Se requiere el ingreso de una descripción no mayor a 100 caracteres")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Se requiere una descripción mayor a 0 caracteres y menor a 100")]
        public required string Description { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Se requiere un estado valido inicialmente como activo")]
        public required bool State {  get; set; }
    }
}
