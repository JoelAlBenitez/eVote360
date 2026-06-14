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
        }
    }
}
