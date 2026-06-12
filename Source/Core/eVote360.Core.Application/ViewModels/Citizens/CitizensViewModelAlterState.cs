using System.ComponentModel.DataAnnotations;

namespace eVote360.Core.Application.ViewModels.Citizens
{
    public sealed class CitizensViewModelAlterState
    {
        [Required(ErrorMessage = "El Id del ciudadano es requerido, Id no reconocido")]
        public required Guid Id { get; set; }

        [Required(ErrorMessage = "El número de identificación es requerido, Identificación no reconocida")]
        public required string Identification { get; set; }
    }
}
