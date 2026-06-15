using eVote360.Core.Domain.Common.Errors;

namespace eVote360.Core.Domain.Common.CodeErrors
{
    public static class IdentificationError
    {
        public static Error IdentificationNoValid
            => new Error("Número de identificación no valido", "El número de identificación ingresado no es valido.");

        public static Error IdentificationNotMatches
            => new Error("Imagen no coincide con la identificación", "El número de identificación visualizado en la imagen no coincide con el registrado anteriormente.");

        public static Error ImgNoValid
            => new Error("Formato invalido", "El formato de la imagen subida no es valida, favor intente de nuevo con un formato valido (jpg, jpeg, png)");

        public static Error ImgNoRead
            => new Error("Imagen no puede ser procesada", "La imagen que ha subido no pudo ser procesada, favor intente  de nuevo en un lugar con más luz y mejor calidad de imagen, recuerde subir unicamente la parte delantera de su documento elctoral.");

        public static Error ImgNoIdentification
            => new Error("Imagen no posee un número de identificación valido", "La imagen subida no posee un número de identicación valido, procese nuevamente la imagen o intente con una imagen diferente.");
    }
}
