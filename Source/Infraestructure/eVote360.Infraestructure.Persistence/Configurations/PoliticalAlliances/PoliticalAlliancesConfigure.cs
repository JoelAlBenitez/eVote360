using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eVote360.Core.Domain.Common.Enums;
using PoliticalAllianceEntity = eVote360.Core.Domain.Entities.PoliticalAlliances.PoliticalAlliances;
using eVote360.Core.Domain.Entities.PoliticalAlliances;
using eVote360.Core.Domain.Entities.User;
namespace eVote360.Infraestructure.Persistence.Configurations.PoliticalAlliances
{
    public class PoliticalAlliancesConfigure : IEntityTypeConfiguration<PoliticalAllianceEntity>
    {
        public void Configure(EntityTypeBuilder<PoliticalAllianceEntity> builder)
        {
            builder.ToTable("PoliticalAlliances");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.RequestingPartyId).IsRequired();
            builder.Property(x => x.ReceivingPartyId).IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .IsRequired()
                .HasDefaultValue(AllianceStatus.Pending);

            builder.Property(x => x.RequestDate)
                .HasColumnType("datetimeoffset(0)")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.ResponseDate)
                .HasColumnType("datetimeoffset(0)");

            builder.Property(x => x.CreateAt)
                .HasColumnType("datetimeoffset(0)")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.CreateUserId).IsRequired();

            // Relaciones comentadas hasta que la entidad de partidos esté disponible
            
            builder.HasOne(x => x.RequestingParty)
                .WithMany()
                .HasForeignKey(x => x.RequestingPartyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ReceivingParty)
                .WithMany()
                .HasForeignKey(x => x.ReceivingPartyId)
                .OnDelete(DeleteBehavior.Restrict);
            

         
            //builder.HasOne(u => u.Users).
            //  WithMany(x => x.PoliticalAlliances).
            //   HasForeignKey(u => u.CreateUserId).
            //   OnDelete(DeleteBehavior.Restrict);
        }
    }
}
