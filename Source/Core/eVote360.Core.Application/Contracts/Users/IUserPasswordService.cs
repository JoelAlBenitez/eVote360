using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Contracts.Users
{
    public interface IUserPasswordService
    {
        string HashPassword(string password);
        bool verifyPassword(string password, string hash);
    }
}
