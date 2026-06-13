using eVote360.Core.Domain.Common.Errors;

namespace eVote360.Core.Domain.Common.CodeErrors
{
    public class LeaderAssignmentErrors
    {
        // Errores de elección activa
        public static Error ActiveElectionExists => new("LASSIGN001", "No se puede crear una asignación de dirigente político mientras exista una elección activa.");
        public static Error ActiveElectionDeleteForbidden => new("LASSIGN002", "No se puede eliminar una asignación mientras exista una elección activa.");

        // Errores de usuario
        public static Error UserNotFound => new("LASSIGN003", "El usuario seleccionado no existe.");
        public static Error UserNotActive => new("LASSIGN004", "El usuario seleccionado no está activo.");
        public static Error UserNotLeaderRole => new("LASSIGN005", "El usuario seleccionado no tiene el rol de dirigente político.");
        public static Error UserAlreadyAssigned => new("LASSIGN006", "Este dirigente ya está relacionado con otro partido político.");

        // Errores de partido
        public static Error PartyNotFound => new("LASSIGN007", "El partido político seleccionado no existe.");
        public static Error PartyNotActive => new("LASSIGN008", "El partido político seleccionado no está activo.");
        public static Error PartyAlreadyHasLeader => new("LASSIGN009", "Este partido político ya tiene un dirigente asignado.");

        // Errores de asignación
        public static Error AssignmentNotFound => new("LASSIGN010", "La asignación seleccionada no existe o ya fue eliminada.");
        public static Error AssignmentNotBelongsToParty => new("LASSIGN011", "No tiene permisos para eliminar esta asignación.");

        // Error genérico
        public static Error UnexpectedError(string msg) => new("LASSIGN999", msg);
    }
}
