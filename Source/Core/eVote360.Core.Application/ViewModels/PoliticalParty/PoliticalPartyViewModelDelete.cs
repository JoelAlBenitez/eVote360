using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Application.ViewModels.PoliticalParty
{
    public sealed class PoliticalPartyViewModelDelete
    {
        [Required(ErrorMessage = "El Id del partido politico es requerido, Id no reconocido")]
        public required int Id { get; set; }

        [Required(ErrorMessage = "El nombre del partido politico es requerido, Nombre no reconocido")]
        public required string Name { get; set; }
    }
}
