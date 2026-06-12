using eVote360.Core.Domain.Commom.BaseEntity;
using eVote360.Core.Domain.Settings.ValueObjects.PoliticalPartyAcronym;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eVote360.Core.Domain.Entities.PoliticalParty
{
    public class PoliticalParty : BaseEntitie<int, string>
    {
        public required string PoliticalPartyDescription { get; set; }
        public required PoliticalPartyAcronym PoliticalPartyAcronym { get; set; }
        public required string PoliticalPartyLogo { get; set; }
    }
}
