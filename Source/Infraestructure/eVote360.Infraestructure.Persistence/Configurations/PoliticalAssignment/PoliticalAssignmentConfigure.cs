using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AssignmentEntity = eVote360.Core.Domain.Entities.PoliticalAssignment.PoliticalAssignment;
using UserEntity = eVote360.Core.Domain.Entities.User.User;
using PartyEntity = eVote360.Core.Domain.Entities.PoliticalParty.PoliticalParty;

namespace eVote360.Infraestructure.Persistence.Configurations.PoliticalAssignment
{
    public class PoliticalAssignmentConfigure : IEntityTypeConfiguration<AssignmentEntity>
    {
        public void Configure(EntityTypeBuilder<AssignmentEntity> builder)
        {
            builder.ToTable("PoliticalAssignments");
            
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1);
            
            builder.Property(x => x.PoliticalLeaderId)
                .IsRequired();
            
            builder.Property(x => x.PoliticalPartyId)
                .IsRequired();
            
            builder.Property(x => x.PolitcalAssignmentDate)
                .IsRequired();
            
            builder.Property(x => x.Name)
                .HasMaxLength(150)
                .HasColumnType("nvarchar")
                .IsRequired();
            
            // Relación con el Dirigente Político
            builder.HasOne(x => x.PoliticalLeader)
                   .WithMany(u => u.PoliticalAssignments) 
                   .HasForeignKey(x => x.PoliticalLeaderId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relación con el Partido Político
            builder.HasOne(x => x.PoliticalParty)
                   .WithMany(p => p.Assignments)
                   .HasForeignKey(x => x.PoliticalPartyId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
