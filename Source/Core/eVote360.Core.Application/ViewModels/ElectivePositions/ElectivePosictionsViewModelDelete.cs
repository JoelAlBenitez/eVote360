using System.ComponentModel.DataAnnotations;

namespace eVote360.Core.Application.ViewModels.ElectivePositions
{
    public sealed class ElectivePosictionsViewModelDelete
    {
        [Required(ErrorMessage = "El Id de la posición electiva es requerido, Id no reconocido")]
        public required int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la posición electiva es requerida, Nombre no reconocido")]
        public required string Name { get; set; }
    }
}
