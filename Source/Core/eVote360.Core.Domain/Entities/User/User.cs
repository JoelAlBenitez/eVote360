using eVote360.Core.Domain.Enums;
using eVote360.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Entities.User
{
    public class User
    {
        public int UserId { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public UserEmail UserEmail { get; set; }

        public Username Username { get; set; }

        public UserPassword UserPassword { get; set; }

        public UserRole UserRole { get; set; }

        public bool UserState { get; set; }   
    }
}
