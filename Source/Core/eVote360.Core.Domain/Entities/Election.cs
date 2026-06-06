
using eVote360.Core.Domain.Enums;
using eVote360.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Entities
{
    public class Election
    {
        public int ElectionId { get; set; }

        public ElectionName ElectionName { get; set; }

        public ElectionDate ElectionDate { get; set; }

        public ElectionState ElectionState = ElectionState.Pendiente;


    }
}
