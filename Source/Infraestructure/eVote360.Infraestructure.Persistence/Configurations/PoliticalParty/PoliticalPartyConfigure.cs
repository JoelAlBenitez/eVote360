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

            builder.OwnsOne(x => x.PoliticalPartyLogo, logo =>
            {
                logo.Property(l => l.PhotoUrl)
                .HasColumnName("PoliticalPartyLogo")
                .IsRequired();
            });

            builder.Property(x => x.State)
                .IsRequired()
                .HasDefaultValue(true);

           
            builder.OwnsOne(x => x.PoliticalPartyAcronym, acronym => {

                acronym.Property(c => c.Value)
                .HasColumnName("PoliticalPartyAcronym")
                .IsRequired();

                acronym.HasIndex(c => c.Value).IsUnique();
            });

            builder.HasIndex(x => x.Name).IsUnique();

            
             builder.HasMany(x => x.RequestedAlliances)
               .WithOne(x => x.RequestingParty) 
               .HasForeignKey(x => x.RequestingPartyId)
               .OnDelete(DeleteBehavior.Restrict);
            
               
                builder.HasMany(x => x.ReceiveAlliances)
                .WithOne(x => x.ReceivingParty)
                .HasForeignKey(x => x.ReceivingPartyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.UserCreate)
                .WithMany()
                .HasForeignKey(x => x.CreateUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.UserUpdate)
                .WithMany()
                .HasForeignKey(x => x.UpdateUserId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
