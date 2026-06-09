using System.ComponentModel.DataAnnotations;

namespace eVote360.Core.Application.ViewModels.ElectivePositions
{
    public sealed class ElectivePosictionsViewModelEdit
    {

        [Required(ErrorMessage = "El Id del la posición electivo es requrido, Id no reconocido")]
        public required int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido y debe tener una longitud mayor a 0 caracteres y no mayor a 30")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "El nombre debe tener una longitud minima de 5 caracteres y no mayor a 30")]
        public required string Name { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "La descripción es requerida y la misma no puede superar los 100 caracteres")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "La descripción debe tener un valor minimo de 1 un caracter")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Se require un estado valido inicialmente marcado como activo")]
        public required bool State {  get; set; }
    }

}
