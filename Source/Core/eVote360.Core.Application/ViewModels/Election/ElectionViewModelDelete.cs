using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Application.ViewModels.Election
{
    public sealed class ElectionViewModelDelete
    {
        [Required(ErrorMessage = "El Id de la eleccion es requerido, Id no reconocido")]
        public required int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la eleccion es requerido, Nombre no reconocido")]
        public required string Name { get; set; }
    }
}
