using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 
using PoliticalPartyEntity = eVote360.Core.Domain.Entities.PoliticalParty.PoliticalParty; 

namespace eVote360.Infraestructure.Persistence.Configurations.PoliticalParty
{
    public class PoliticalPartyConfigure : IEntityTypeConfiguration<PoliticalPartyEntity>
    {
        public void Configure(EntityTypeBuilder<PoliticalPartyEntity> builder)
        {
            builder.ToTable("PoliticalParties");

            builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .HasColumnType("nvarchar")
                .IsRequired();

            builder.Property(x => x.PoliticalPartyDescription)
                .HasMaxLength(250)
                .HasColumnType("nvarchar")
                .IsRequired();

            builder.Property(x => x.PoliticalPartyLogo.PhotoUrl)
                .HasColumnName("PoliticalPartyLogo")
                .IsRequired();

            builder.Property(x => x.State)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(x => x.PoliticalPartyAcronym.Value)
                .HasColumnName("Acronym")
                .HasColumnType("nvarchar")
                .HasMaxLength(3)
                .IsRequired();

            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasIndex(x => x.PoliticalPartyAcronym.Value).IsUnique();

            
             builder.HasMany(x => x.RequestedAlliances)
               .WithOne() 
               .HasForeignKey(x => x.RequestingPartyId)
               .OnDelete(DeleteBehavior.Restrict);
            
               
                builder.HasMany(x => x.ReceiveAlliances)
                .WithOne()
                .HasForeignKey(x => x.ReceivingPartyId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
