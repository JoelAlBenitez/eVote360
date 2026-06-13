using System.ComponentModel.DataAnnotations;

namespace eVote360.Core.Application.ViewModels.Candidates
{
    public sealed class CandidateViewModelAlterState
    {
        [Required(ErrorMessage = "El Id del candidato es requerido")]
        public required int Id { get; set; }

        [Display(Name = "Nombre Completo")]
        public required string FullName { get; set; }
    }
}
