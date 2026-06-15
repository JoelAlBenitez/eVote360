using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eVote360.Core.Application.ViewModels.CandidateAssignment
{
    public class CreateAssignmentViewModel
    {
        [Required(ErrorMessage = "El puesto electivo es requerido.")]
        public int ElectivePositionId { get; set; }
        
        public string ElectivePositionName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "El candidato es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un candidato válido.")]
        public int CandidateId { get; set; }
        
        public List<SelectListItem> EligibleCandidates { get; set; } = new();
    }
}
