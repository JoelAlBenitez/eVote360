using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.Contracts.ServiceValidates.PoliticalAssignment;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Infraestructure.Persistence.ServicesValidators.PoliticalAssignment
{
    public class PoliticalAssignmentServiceValidator : IPoliticalAssignmentDomainService
    {
        private readonly DbContextEVote360 _context;

        public PoliticalAssignmentServiceValidator(DbContextEVote360 context) {
        _context = context;
        }

        public async Task<bool> IsPartyActiveAsync(int partyId)
            { 
                var party = await _context.PoliticalParties
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == partyId);
                return party != null && party.State;
            
         
            }
   
            public async Task<bool> PartyAlreadyHasLeaderAsync(int partyId)
            {
                return await _context.PoliticalAssignments
                    .AnyAsync(x => x.PoliticalPartyId == partyId && x.State == true);
            }

            public async Task<bool> UserHasLeaderRoleAsync(int userId)
            {
                var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == userId);

                return user != null && user.UserRole == UserRole.DirigentePolitico;
             }

            public async Task<bool> UserIsActiveAsync(int userId)
            {
                 var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == userId);
                 return user != null && user.State;
             }

            public async Task<bool> UserAlreadyAssignedAsync(int userId)
            {
                return await _context.PoliticalAssignments
                .AnyAsync(x => x.PoliticalLeaderId == userId && x.State == true);
            }
    }
}
