using BCrypt.Net;
using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.Contracts.Repositories.AuthenticationAndAutorization;
using eVote360.Core.Domain.Entities.Authentication;
using eVote360.Core.Domain.Entities.User;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
namespace eVote360.Infraestructure.Persistence.Repositories.Authentication
{
    public class AuthenticationRepository : IAuthenticationRepository
    {

        private readonly DbContextEVote360 _context;
        public AuthenticationRepository(DbContextEVote360 context) {
            _context = context;
        }

       
      
        public async Task<UserAuthenticate> ReturnUserFindAsync(string username, string password)
        {
            var result = (await _context.Users
               .AsNoTracking()
               .FirstOrDefaultAsync(u => u.Name == username));
            if (result == null) return null!;
            bool passwordValid = result.UserPassword.HashValue == password;
            if (!passwordValid) return null!;

            int? partyId = null;
            if (result.UserRole == UserRole.DirigentePolitico)
            {
                var party = await _context.PoliticalAssignments
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.PoliticalLeaderId == result.Id);
                if (party == null) return null!;
                partyId = party.Id;
            }

            return new UserAuthenticate {
                IdUser = result.Id,
                NameUser = result.Name,
                PoliticalPartyId = partyId,
                Role = result.UserRole,
                state = result.State,
            };
           
        }
    }

  }

