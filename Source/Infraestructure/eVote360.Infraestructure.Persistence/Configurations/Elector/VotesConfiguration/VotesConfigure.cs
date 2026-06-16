

using eVote360.Core.Domain.Entities.Elector.Vote;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote360.Infraestructure.Persistence.Configurations.Elector.VotesConfiguration
{
    public class VotesConfigure : IEntityTypeConfiguration<Votes>
    {
        public void Configure(EntityTypeBuilder<Votes> builder)
        {
            builder.HasKey(x => x.Id);

 

            builder.HasOne(x => x.ElectivePosition)
                .WithMany()
                .HasForeignKey(x => x.IdElectivePosiction)
                .OnDelete(DeleteBehavior.Restrict);
           
            builder.HasOne(x => x.Elections)
                .WithMany()
                .HasForeignKey(x => x.IdElection)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Candidacte)
                .WithMany()
                .HasForeignKey(x => x.IdCandidate)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
