

using eVote360.Core.Domain.Entities.Elector.CodeVerifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote360.Infraestructure.Persistence.Configurations.Elector.CodeVerificationConfigurations
{
    public class CodeVerificationConfigure : IEntityTypeConfiguration<CodeVerification>
    {
        public void Configure(EntityTypeBuilder<CodeVerification> builder)
        {
            builder.ToTable("CodeVerification");
            builder.HasKey(e => new { e.IdCitizens, e.IdElection });

            builder.Property(e => e.Code).IsRequired();
            builder.Property(e => e.State).IsRequired();
         

            builder.Property(e => e.CreateAt).HasColumnType("datetimeoffset").HasDefaultValueSql("SYSDATETIMEOFFSET()").IsRequired();
            builder.Property(e => e.ExpirationDate).HasColumnType("datetimeoffset").IsRequired();

            builder.HasOne(el => el.election)
                .WithMany()
                .HasForeignKey(e => e.IdElection)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Citizen)
                .WithMany()
                .HasForeignKey(e => e.IdCitizens)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
