using eVote360.Core.Domain.Entities.Elector.AuditVote;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote360.Infraestructure.Persistence.Configurations.Elector.AuditVotesCofigurations
{
    public class AuditVotesConfigure : IEntityTypeConfiguration<AuditVotes>
    {
        public void Configure(EntityTypeBuilder<AuditVotes> builder)
        {
            builder.ToTable("AuditVotes");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.IdCitizen).IsRequired();
            builder.Property(e => e.IdElection).IsRequired();

            builder.Property(e => e.CreatAt).IsRequired()
                .HasColumnType("datetimeoffset(0)").
                HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(e => e.ElectionEntities)
                .WithMany()
                .HasForeignKey(e => e.IdElection)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Citizens)
                .WithMany()
                .HasForeignKey(e => e.IdCitizen)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
