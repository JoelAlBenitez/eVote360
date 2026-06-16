using eVote360.Core.Domain.Common.Errors;

namespace eVote360.Core.Domain.Common.CodeErrors
{
    public static  class VotesError
    {
        public static Error DataInvalid
        => new Error("Datos invalidos", "Oh al parecer no has seleccionados los diversos puestos!!!,  favor revisar los diversos puestos y candidactos seleccionados.");

        public static Error ExistVotes
            => new Error("Proceso de votación ya existente", "Estimado ciudadano usted ya pose un proceso de votaciones realizados en estas elecciones, " +
                "favor revisar su resumen electoral en su bandeja de email.");

        public static Error UnexpectedError
            => new Error("Oh un error inesperado ha ocurrido", "Un error inesperado a ocurrido en el procesamiento de la voleta, favor intenta de nuevo.");

        public static Error ElectoralProcessNoValid
            => new Error("No hay un procesor electoral en este momento", "Ningun procesos electoral siendo llevado a cabo en estos momentos, si se trata de un error favor contactar con un admnistrador por lo medios pertinentes.");

        //.....
    }
}
