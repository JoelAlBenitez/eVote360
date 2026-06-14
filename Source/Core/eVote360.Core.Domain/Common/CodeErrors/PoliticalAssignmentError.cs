using eVote360.Core.Domain.Common.Errors;

namespace eVote360.Core.Domain.Common.CodeErrors
{
    public static class PoliticalAssignmentError
    {
        public static Error PartyIsNotActivated
           => new Error("Asignacion no creada", "No se puede crear la asignacion del lider politico a un partido no activado.");

        public static Error HasAlreadyAnAssignment
   => new Error("Asignacion no creada", "No se puede crear la asignacion del lider politico porque este ya es lider de un partido.");

        public static Error HasAlreadyALeader
   => new Error("Asignacion no creada", "No se puede crear la asignacion del lider politico a un partido que ya posee un lider politico.");

        public static Error PartyDoesntExist
  => new Error("Asignacion no creada", "No se puede crear la asignacion del lider politico a un partido que no existe.");

        public static Error LeaderDoesntExist
  => new Error("Asignacion no creada", "No se puede crear la asignacion con un lider politico inexistente.");

        public static Error UserIsNotLeader
=> new Error("Asignacion no creada", "No se puede crear la asignacion de lider politico a un usuario el cual no tiene el rol de lider politico.");

        public static Error UserIsNotActive
=> new Error("Asignacion no creada", "No se puede crear la asignacion de lider politico con un usuario inactivo.");

    }
}
