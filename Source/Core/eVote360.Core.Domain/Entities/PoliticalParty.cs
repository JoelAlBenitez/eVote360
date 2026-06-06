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
        public int PoliticalpartyId { get; private set; }
        public string PoliticalPartyName { get; private set; }
        public string PoliticalPartyDescription { get; private set; }
        public PoliticalPartyAcronym PoliticalPartyAcronym { get; private set; }
        public string PoliticalPartyLogo { get; private set; }
        public bool PoliticalPartyState { get; private set; }

        private PoliticalParty(){ }


        public PoliticalParty(string politicalPartyName, string politicalPartyDescription, PoliticalPartyAcronym politicalPartyAcronym, string politicalPartyLogo, bool politicalPartyState)
        {
            if (string.IsNullOrWhiteSpace(politicalPartyName))
                throw new ArgumentException("El nombre del partido es requerido.");

            if (string.IsNullOrWhiteSpace(politicalPartyLogo))
                throw new ArgumentException("El logo del partido es requerido.");

            PoliticalPartyName = politicalPartyName.Trim();
            PoliticalPartyDescription = politicalPartyDescription;
            PoliticalPartyAcronym = politicalPartyAcronym;
            PoliticalPartyLogo = politicalPartyLogo;
            PoliticalPartyState = true;
        }


        public void ChangePoliticalPartyState(bool newState)
        {
            PoliticalPartyState = newState;
        }

        public void UpdatePoliticalPartyInfo(string newPoliticalPartyName, string newPoliticalPartyDescription,string newPoliticalPartyLogo, PoliticalPartyAcronym newPoliticalPartyAcronym, bool HasParticipatedInElections)
        {
            if (HasParticipatedInElections) 
            {
                throw new InvalidOperationException("No se pueden modificar los datos del partido político una vez creado.");
            }

            if (string.IsNullOrWhiteSpace(newPoliticalPartyName))
                throw new ArgumentException("El nombre del partido es requerido.");

            PoliticalPartyName = newPoliticalPartyName.Trim();
            PoliticalPartyDescription = newPoliticalPartyDescription;
            PoliticalPartyAcronym = newPoliticalPartyAcronym;

            if (!string.IsNullOrWhiteSpace(newPoliticalPartyLogo))
            {
                PoliticalPartyLogo = newPoliticalPartyLogo;
            }
        }

    }
}
