using eVote360.Core.Application.ViewModels.Base;
using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Application.ViewModels.Election
{
    public sealed class ElectionCreateViewModel 

    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Se requiere un nombre valido")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Ingrese un nombre de eleccion mayor a 5 caracteres y no mayor a 30")]

        public string ElectionName { get; set; }

        [Display(Name = "Fecha de Eleccion")]
        [Required(ErrorMessage = "Se requiere una fecha valida")]
        public DateTime ElectionDate { get; set; }


    }
}
