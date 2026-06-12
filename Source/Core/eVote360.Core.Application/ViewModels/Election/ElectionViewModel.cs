using eVote360.Core.Application.ViewModels.Base;
using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.Settings.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Application.ViewModels.Election
{
    public sealed class ElectionViewModel : ViewModelBase<int>
    {
        public DateTime ElectionDate { get; set; }
        public ElectionState ElectionState { get; set; }
    }
}
