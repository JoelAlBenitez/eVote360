using eVote360.Core.Domain.Common.Errors;


namespace eVote360.Core.Domain.Common.CodeErrors
{
    public static class ElectionError
    {
        public static Error ElectionDateNotValid
           => new Error("Fecha de elecciones no valida", "La fecha de las elecciones tiene que ser valida.");

        public static Error ElectionNameNotValid
          => new Error("Nombre de elecciones no valido", "El nombre de las elecciones tiene que ser valido.");

        public static Error ElectionNameAlreadyExist
          => new Error("Nombre de elecciones no valido", "El nombre de las elecciones ya existe y no se puede repetir.");

        public static Error ElectionStateError
          => new Error("Estado de elecciones no valido", "No se pueden activar unas elecciones mientras hayan otras activas.");
         public static Error ElectionCreationNotValid
           => new Error("Creacion de elecciones no valida", "La eleccion debe tener minimo 2 partidos politicos para poder comenzar.");

        public static Error ElectionActive
         => new Error("Elecciones ya iniciadas", "Hay un proceso electivo iniciado por lo que ninguna de estas operaciones se encuentra disponible de momento.");
    }
}
