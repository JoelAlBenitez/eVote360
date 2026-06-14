using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ElectionEntitie = eVote360.Core.Domain.Entities.Election.Election;

namespace eVote360.Infraestructure.Persistence.Configurations.Election
{
    public class ElectionConfigure : IEntityTypeConfiguration<ElectionEntitie>
    {
        public void Configure(EntityTypeBuilder<ElectionEntitie> builder) {


            builder.ToTable("Elections");
            
                builder.HasKey(x => x.Id);
                         builder.Property(x => x.Id)
                         .UseIdentityColumn(1, 1);
            
                builder.Property(x => x.Name)
                         .HasMaxLength(100)
                         .HasColumnType("nvarchar")
                         .IsRequired();           

                builder.Property(x => x.ElectionDate.Value)
                         .HasColumnName("ElectionDate")
                         .IsRequired();
            
                builder.Property(x => x.ElectionState)
                         .IsRequired();
            
                builder.Property(x => x.State)
                         .IsRequired()
                         .HasDefaultValue(true);

                builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
