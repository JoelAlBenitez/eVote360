using eVote360.Core.Domain.Common.Errors;
namespace eVote360.Core.Domain.Common.CodeErrors
{
    public static class CandidatesError
    {
        public static Error NameInvalid
                   => new Error("Nombre inválido", "El nombre del candidato no puede estar vacío y solo debe contener letras.");


        public static Error LastNameInvalid => new Error("Apellido inválido", "El apellido del candidato no puede estar vacío y solo debe contener letras.");

        public static Error PhotoInvalid => new Error("Foto inválida", "La foto del candidato es obligatoria y debe ser un archivo .jpg, .jpeg o .png.");

        public static Error CandidateHasParticipatedInElection => new Error("Candidato con historial electoral", "No se pueden modificar los datos de un candidato que ya ha participado en procesos electorales pasados.");

        public static Error ActiveElectionExists => new Error("Elección activa", "No se pueden realizar operaciones de mantenimiento mientras exista una elección en curso.");

        public static Error CandidateAssignedToPosition => new Error("Candidato con Puesto Asignado.", "No se puede desactivar el candidato porque ya tiene un puesto electivo vinculado.");

        public static Error PoliticalPartyNotActive
                 => new Error("Partido inactivo", "No se pueden gestionar candidatos porque el partido político no se encuentra activo.");

        public static Error DataInvalid
        => new Error("Datos no válidos", "Se han introducido datos no válidos en el intento operacional.");

        public static Error NameAlreadyExists
        => new Error("Nombre duplicado", "Ya existe un candidato con ese nombre y apellido en el partido.");


        public static Error CandidateNotBelongsToParty
                    => new Error("Acceso denegado", "El candidato no pertenece a su partido político.");

        public static Error NoPartyAssigned
    => new Error("Sin partido asignado", "No puede gestionar candidatos porque no tiene un partido político asignado.");

        public static Error CandidateAlreadyActive
            => new Error("Candidato ya activo", "Este candidato ya se encuentra activo.");

public static Error CandidateAlreadyInactive
    => new Error("Candidato ya inactivo", "Este candidato ya se encuentra inactivo.");
    }
}
