using eVote360.Core.Domain.Entities.Elector.Vote;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote360.Infraestructure.Persistence.Configurations.Elector.VotesConfigurations
{
    public class VotesConfigure : IEntityTypeConfiguration<Votes>
    {
        public void Configure(EntityTypeBuilder<Votes> builder)
        {
            builder.ToTable("Votes");
            
            // Primary Key
            builder.HasKey(v => v.Id);

            builder.Property(v => v.IdElection).IsRequired();
            builder.Property(v => v.IdElectivePosiction).IsRequired();
            builder.Property(v => v.IdCandidate).IsRequired(false);

            // Relationships
            builder.HasOne(v => v.Elections)
                .WithMany(e => e.Votes)
                .HasForeignKey(v => v.IdElection)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.ElectivePosition)
                .WithMany()
                .HasForeignKey(v => v.IdElectivePosiction)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.Candidacte)
                .WithMany()
                .HasForeignKey(v => v.IdCandidate)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
