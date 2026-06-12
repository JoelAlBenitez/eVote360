using eVote360.Core.Application.DTOs.Base;
using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.Settings.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Application.DTOs.Users
{
    public record UsersDto : BaseDto<int>
    {
        public required string UserFirstName { get; set; }

        public required string UserLastName { get; set; }

        public required string UserEmail { get; set; }
        public required string UserPassword { get; set; }

        public UserRole UserRole { get; set; }

        public DateTimeOffset? CreateAt { get; set; }

        public  DateTimeOffset? UpdateAt { get; set; }

        public  int? CreateUserId { get; set; }

        public  int? UpdateUserId { get; set; }
    }
}
