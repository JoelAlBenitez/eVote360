using eVote360.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eVote360.Core.Domain.Entities
{
    public class PoliticalParty
    {
        public int PoliticalpartyId { get; set; }
        public string PoliticalPartyName { get; set; }
        public string PoliticalPartyDescription { get; set; }
        public PoliticalPartyAcronym PoliticalPartyAcronym { get; set; }
        public string PoliticalPartyLogo { get; set; }
        public bool PoliticalPartyState { get; set; }
        private PoliticalParty(){ }
    }
}
