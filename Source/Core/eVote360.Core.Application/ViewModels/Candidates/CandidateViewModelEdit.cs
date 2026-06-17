using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace eVote360.Core.Application.ViewModels.Candidates
{
    public sealed class CandidateViewModelEdit
    {
        [Required(ErrorMessage = "El Id del candidato es requerido")]
        public required int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres")]
        public required string Name { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El apellido es requerido")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El apellido debe tener entre 2 y 50 caracteres")]
        public required string LastName { get; set; }
[Display(Name = "Foto")]
public IFormFile? PhotoFile { get; set; }

public string? CurrentPhotoUrl { get; set; }

public bool HasParticipatedInElection { get; set; }

     
    }
}
  

