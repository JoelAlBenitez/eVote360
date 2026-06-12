using eVote360.Core.Domain.Common.Errors;

namespace eVote360.Core.Domain.Common.CodeErrors
{
    public static class CitizenErrors
    {
        public static Error DataInvalid
            => new Error("Datos incongruentes", "Los campos llenados presentan datos incoherentes, favor verificar cada campo.");

        public static Error StateNoValidOfModifie
            => new Error("Modificación de estado no valida", "Para modificar el estado del ciudadano el estado anterior debe ser diferente al estado al que se quiere modificar.");

        public static Error ExistCitizen
            => new Error("Ciudadano ya registrado", "Este ciudadano ya se encuentra registrado en el sistema, " +
                "consulte el listado de identificaciones y correos para continuar con el registro.");
        public static Error NameNoValid
            => new Error("Nombre no valido", "El nombre que ha ingresado no es valido, el misno no debe tener mas de 40 caracteres ni caracteres especiales.");

        public static Error LastNameNoValid
            => new Error("Apellido no valido", "El apellido que ha ingreaso no es valido, el mismo no debe tener mas de 40 caracteres ni caracteres especiales.");

        public static Error StateNoValid
            => new Error("Estado inciial no valido", "El estado inicial del ciudadano debe ser activo al momento de su creación.");

        public static Error DesactiveNoValid
            => new Error("Estado no puede ser alterado", "El estado del ciudadano no puede pasar a desactivado mientras haya una elección activa.");

        public static Error ChangeIdentificationNoValid
            => new Error("Cambios no validos", "El ciudadano ya ha participado en un proceso electoral por lo que no se puede alterar su número de identificación.");

        public static Error ExistIdentification
            => new Error("Identificación ya registrada", "El número de identificación ingresado ya se encuentra registrado en el sistema.");

        public static Error ExistEmail
            => new Error("Correo Electrónico ya registrado", "El correo electrónico ingresado ya se encuentra registrado en el sistema.");

        public static Error NoExtisCitizen
            => new Error("Ciudadano no encontrado", "El ciudadano sobre el cual se quiere operar no fue encontrado en el sistema, favor verificar o intentar de nuevo.");

        public static Error ActiveNoValid
            => new Error("Estado no puede ser alterado", "El estado del ciudadano no puede pasar a activado mientras exista otro ciudadano con esta identificación o en su defecto elecciones activas.");
    }
}
