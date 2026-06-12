using eVote360.Core.Domain.Commom.BaseEntity;
using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.Settings.ValueObjects.ElectionDate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Entities.Election
{
    public class Election : BaseEntitie<int, string>
    {

        public ElectionDate ElectionDate { get; set; }

        public ElectionState ElectionState { get; set; }
    }
}
