using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eVote360.Core.Domain.Entities.PoliticalLeaderAssignment;

namespace eVote360.Infraestructure.Persistence.Configurations.PoliticalLeaderAssignment
{
    public class PoliticalLeaderAssignmentConfigure : IEntityTypeConfiguration<Core.Domain.Entities.PoliticalLeaderAssignment.PoliticalLeaderAssignment>
    {
        public void Configure(EntityTypeBuilder<Core.Domain.Entities.PoliticalLeaderAssignment.PoliticalLeaderAssignment> builder)
        {
            builder.ToTable("PoliticalLeaderAssignments");
            builder.HasKey(x => x.Id);

            // UserId — requerido, índice único (un dirigente solo puede tener un partido)
            builder.Property(x => x.UserId).IsRequired();
            builder.HasIndex(x => x.UserId).IsUnique();

            // PoliticalPartyId — requerido, índice único (un partido solo puede tener un dirigente)
            builder.Property(x => x.PoliticalPartyId).IsRequired();
            builder.HasIndex(x => x.PoliticalPartyId).IsUnique();

            builder.Property(x => x.CreateAt)
                .HasColumnType("datetimeoffset(0)")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.CreateUserId).IsRequired();

            // FKs comentadas estilo Joel
            /*
            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.PoliticalParty)
                .WithMany()
                .HasForeignKey(x => x.PoliticalPartyId)
                .OnDelete(DeleteBehavior.Restrict);
            */
        }
    }
}
