using eVote360.Core.Application.ViewModels.Base;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace eVote360.Core.Application.ViewModels.PoliticalParty
{
    public sealed class PoliticalPartyViewModelEdit
    {


        [Display(Name = "Id")]
        [Required(ErrorMessage = "Se requiere un Id valido")]
        public required int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Se requiere un nombre valido")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Ingrese un nombre valido de minimo 5 y maximo 20 caracteres")]
        public required string Name { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Se requiere estado valido")]
        public required bool State { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "Se requiere una descripcion valida")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Ingrese un una descripcion valida de minimo 10 y maximo 500 caracteres")]
        public required string PoliticalPartyDescription { get; set; }

        [Display(Name = "Siglas")]
        [Required(ErrorMessage = "Se requiere unas siglas validas")]
        [StringLength(5, MinimumLength = 2, ErrorMessage = "Ingrese unas siglas de minimo 2 letras y maximo de 5")]
        public required string PoliticalPartyAcronym { get; set; }

        [Display(Name = "Logo")]
        [Required(ErrorMessage = "Se requiere un Logo de partido valido")]
        
        public required string PoliticalPartyLogo { get; set; }

        public required IFormFile? LogoFile { get; set; }
    }
}
