using System.ComponentModel.DataAnnotations;

namespace eVote360.Core.Application.ViewModels.PoliticalLeaderAssignment
{
    public sealed class LeaderAssignmentCreateViewModel
    {
        [Display(Name = "Id Lider Politico")]
        [Required(ErrorMessage = "Se requiere un Id valido")]
        public required int PoliticalLeaderId { get; set; }


        [Display(Name = "Id Partido Politico")]
        [Required(ErrorMessage = "Se requiere un Id de Partido Politico valido")]
        public required int PoliticalPartyId { get; set; }

        [Display(Name = "Fecha de Asignacion")]
        [Required(ErrorMessage = "Se requiere una fecha valida para asignar un lider")]
        public required DateTime PoliticalAssignmentDate { get; set; }
    }
}
