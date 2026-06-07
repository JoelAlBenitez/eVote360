using eVote360.Core.Domain.Commom.BaseEntity;
using eVote360.Core.Domain.ValueObjects;
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
        public string PoliticalPartyDescription { get; set; }
        public PoliticalPartyAcronym PoliticalPartyAcronym { get; set; }
        public string PoliticalPartyLogo { get; set; }
        public bool PoliticalPartyState { get; set; }
        private PoliticalParty(){ }
    }
}
