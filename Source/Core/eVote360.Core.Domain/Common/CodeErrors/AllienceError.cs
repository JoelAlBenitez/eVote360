using eVote360.Core.Domain.Common.Errors;

namespace eVote360.Core.Domain.Common.CodeErrors
{
    public static class AllianceErrors
    {
        public static Error CannotModifyDuringActiveElection
            => new Error("Elección activa", "No se pueden modificar alianzas políticas mientras exista una elección activa.");

        public static Error CannotAllyWithOwnParty
            => new Error("Partido propio", "No puede crear una solicitud de alianza hacia su propio partido político.");

        public static Error AllianceAlreadyExists
            => new Error("Alianza vigente", "Ya existe una alianza vigente con este partido político.");

        public static Error PendingRequestAlreadyExists
            => new Error("Solicitud pendiente", "Ya existe una solicitud de alianza pendiente entre ambos partidos.");

        public static Error CannotDeleteWithAssignedCandidates
            => new Error("Candidatos asignados", "No se puede eliminar esta alianza porque existen candidatos aliados asignados entre estos partidos.");

        public static Error AllianceNotFound
            => new Error("No encontrada", "La alianza política seleccionada no existe o ya fue eliminada.");

        public static Error RequestNotFound
            => new Error("No encontrada", "La solicitud de alianza seleccionada no existe o ya fue eliminada.");

        public static Error NoPermission
            => new Error("Sin permisos", "No tiene permisos para realizar esta acción sobre esta solicitud o alianza.");

        public static Error RequestAlreadyAnswered
            => new Error("Ya respondida", "Esta solicitud de alianza ya fue respondida.");

        public static Error CannotDeleteAcceptedRequest
            => new Error("Solicitud aceptada", "No se puede eliminar una solicitud aceptada porque ya generó una alianza vigente.");

        public static Error PartyNotActive
            => new Error("Partido inactivo", "No puede crear una solicitud de alianza con un partido político inactivo.");

        public static Error DataInvalid
            => new Error("Datos no válidos", "Se han introducido datos no válidos en el intento operacional.");
    }
}