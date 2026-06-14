using eVote360.Core.Application.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Application.ViewModels.PoliticalParty
{
    public sealed class PoliticalPartyViewModel : ViewModelBase<int>
    {
        public required string PoliticalPartyDescription { get; set; }
        public required string PoliticalPartyAcronym { get; set; }
        public required string PoliticalPartyLogo { get; set; }
    }
}
