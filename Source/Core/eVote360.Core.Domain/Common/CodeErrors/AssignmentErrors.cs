using eVote360.Core.Domain.Common.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Common.CodeErrors
{
    public class AssignmentErrors
    {
        // Errores de acceso
        public static Error UnauthorizedAccess => new("AUTH001", "No tiene permisos para realizar esta operación.");
        public static Error PartyNotAssigned => new("AUTH002", "No tiene un partido político asignado.");
        public static Error PartyNotActive => new("AUTH003", "El partido político asignado está inactivo.");

        // Errores de elección activa
        public static Error ActiveElectionExists => new("ASSIGN001", "No se puede asignar candidatos a puestos mientras exista una elección activa.");
        public static Error ActiveElectionDeleteForbidden => new("ASSIGN002", "No se puede eliminar una asignación mientras exista una elección activa.");

        // Errores de candidato
        public static Error CandidateNotFound => new("ASSIGN003", "El candidato seleccionado no existe.");
        public static Error CandidateNotActive => new("ASSIGN004", "El candidato seleccionado no está activo.");
        public static Error CandidateAlreadyAssigned => new("ASSIGN005", "Este candidato ya está asignado a un puesto dentro del partido.");

        // Errores de puesto electivo
        public static Error ElectivePositionNotFound => new("ASSIGN006", "El puesto electivo seleccionado no existe.");
        public static Error ElectivePositionNotActive => new("ASSIGN007", "El puesto electivo seleccionado no está activo.");
        public static Error ElectivePositionAlreadyOccupied => new("ASSIGN008", "Este puesto electivo ya tiene un candidato asignado dentro del partido.");

        // Errores de candidato aliado
        public static Error NoActiveAlliance => new("ASSIGN009", "No existe una alianza vigente con el partido de este candidato.");
        public static Error AlliedCandidateHasNoPosition => new("ASSIGN010", "Este candidato aliado no tiene un puesto asignado en su partido de origen.");
        public static Error AlliedCandidateDifferentPosition => new("ASSIGN011", "Este candidato en su partido de origen aspira a un puesto diferente al seleccionado.");

        // Errores de asignación
        public static Error AssignmentNotFound => new("ASSIGN012", "La asignación seleccionada no existe o ya fue eliminada.");
        public static Error AssignmentNotBelongsToParty => new("ASSIGN013", "No tiene permisos para eliminar esta asignación.");

        // Error genérico
        public static Error UnexpectedError(string msg) => new("ASSIGN999", msg);
    }
}
