using Microsoft.EntityFrameworkCore;
using eVote360.Core.Domain.Entities.ElectivePosition;
using eVote360.Core.Domain.Entities.Candidate;
using eVote360.Core.Domain.Entities.PoliticalAlliances;
using eVote360.Core.Domain.Entities.CandidateAssignment;
using System.Reflection;
using eVote360.Core.Domain.Entities.Citizens;
using eVote360.Core.Domain.Entities.User;
using eVote360.Core.Domain.Entities.PoliticalAssignment;

using eVote360.Core.Domain.Entities.Election;
using eVote360.Core.Domain.Entities.PoliticalParty;

using eVote360.Core.Domain.Entities.PoliticalParty;
using eVote360.Core.Domain.Entities.Election;


namespace eVote360.Infraestructure.Persistence.Context
{
    public class DbContextEVote360 : DbContext
    {
        public DbContextEVote360(DbContextOptions<DbContextEVote360> options) : base(options) { }

        public DbSet<ElectivePositions> ElectivePosition { get; set; }
        public DbSet<Candidates> Candidates { get; set; }
        public DbSet<Citizen> Citzens { get; set; }
        public DbSet<PoliticalAlliances> PoliticalAlliances { get; set; }
        public DbSet<CandidateAssignment> CandidateAssignments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PoliticalAssignment> PoliticalAssignments { get; set; }
        public DbSet<Election> Elections { get; set; }
        public DbSet<PoliticalParty> PoliticalParties { get; set; }


        public DbSet<Election> Elections{ get; set; }

        public DbSet<PoliticalParty> PoliticalParties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
