using eVote360.Core.Domain.Common.Errors;

namespace eVote360.Core.Domain.Common.CodeErrors
{
    public static class CodeVerificationError
    {
        public static Error NoExistCodeVerification
            => new Error("Codigó no valido", "El codigó ingresado no existe, favor intente de nuevo.");

        public static Error InvalidCodeVerification
            => new Error("Codigó ya utilizado", "El codigó ingresado ya fue utilizado, favor intente de nuevo.");

        public static Error CodeAlreadyUsed
            => new Error("Codigó expirado", "El codigó ingresado se encuentra vencido, favor intente de nuevo o solicite otro codigó.");

        public static Error CodeInvalid
            => new Error("Codigó con formato invalido", "El codigó ingresado no tiene un formato valido, el mismo debe ser de 6 digitos númericos.");

        public static Error CodeVerificationActive
            => new Error("Codigó de verificación ya solicitado", "Usted ya posee un codigó de verificación solicitado, por lo que debe esperar 5 minutos para solicitar otro codigó.");

        public static Error CodeVerificationFailed
            => new Error("Codigó no existente", "El codigó ingresado no fue encontrado para el ciudadano presente.");
    }
}
