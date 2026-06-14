using eVote360.Core.Domain.Common.Errors;

namespace eVote360.Core.Domain.Common.CodeErrors
{
    public static class UserErrors
    {
        public static Error FirstNameRequired
            => new Error("Insercion no permitida", "El primer nombre del usuario es requerido.");

        public static Error LastNameRequired
            => new Error("Insercion no permitida", "El apellido del usuario es requerido.");

        public static Error EmailNotValid
    => new Error("Insercion no permitida", "El formato de Email introducido no es valido.");

        public static Error EmailAlreadyExist
    => new Error("Insercion no permitida", "El email introducido ya esta registrado");

        public static Error UsernameAlreadyExist
    => new Error("Insercion no permitida", "El username introducido ya esta registrado.");

        public static Error LastAdminDesactivation
    => new Error("Desactivacion no permitida", "No se puede desactivar el ultimo administrador.");

        public static Error SelfDesactivation
    => new Error("Desactivacion no permitida", "No puedes desactivarte a ti mismo.");
    }
}
