using Microsoft.EntityFrameworkCore;
using eVote360.Core.Domain.Entities.ElectivePosition;
using System.Reflection;
using eVote360.Core.Domain.Entities.Citizens;

namespace eVote360.Infraestructure.Persistence.Context
{
    public class DbContextEVote360 : DbContext
    {
        public DbSet<ElectivePositions> ElectivePosition { get; set; }

        public DbSet<Citizen> Citzens { get; set; }
        
        public DbContextEVote360(DbContextOptions<DbContextEVote360> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}