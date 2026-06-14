using eVote360.Core.Domain.Common.Errors;

namespace eVote360.Core.Domain.Common.CodeErrors
{
    public static class AuthenticationAuthorizationError
    {
        public static Error UserNoFind
            => new Error("Usuario no encontrado", "El usuario ingresado no existe en el sistema, favor intente de nuevo. ");

        public static Error DoesNotHaveEnoughPrivileges
            => new Error("Priviligecios insuficientes", "No tiene privilegios suficentes para la acción que ha intentando realizar.");

        //....

    }
}
