namespace eVote360.Core.Application.CandidateAssignment.DTOs
{
    public class CandidateAssignmentDto
    {
        // Datos del Puesto Electivo (Siempre tendrán valor)
        public int ElectivePositionId { get; set; }
        public string ElectivePositionName { get; set; } = string.Empty;
        
        // Datos de la Asignación / Candidato (Nulables porque puede estar Vacante)
        public int? AssignmentId { get; set; } // El Id de la asignación (para poder eliminarla)
        public int? CandidateId { get; set; }
        public string? CandidateName { get; set; }
        public string? CandidateLastName { get; set; }
        public string? PhotoUrl { get; set; } // Requerido por el documento
        
        // Datos de Alianza
        public string? CandidateType { get; set; } // "Propio", "Aliado" o null
        public string? OriginPartyName { get; set; }
        public string? OriginPartySiglas { get; set; }
        
        public int AssigningPartyId { get; set; }
    }
}
