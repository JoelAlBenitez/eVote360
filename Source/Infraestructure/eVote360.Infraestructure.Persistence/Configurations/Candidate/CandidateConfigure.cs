using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eVote360.Core.Domain.Entities.Candidate;

namespace eVote360.Infraestructure.Persistence.Configurations.Candidate
{
    public class CandidateConfigure : IEntityTypeConfiguration<Candidates>
    {
        public void Configure(EntityTypeBuilder<Candidates> builder)
        {
            builder.ToTable("Candidates");
            builder.HasKey(x => x.Id);

            // Mapeo del Value Object FullName (heredado como 'Name')
            builder.OwnsOne(x => x.Name, name =>
            {
                name.Property(n => n.Name)
                    .HasColumnName("FirstName")
                    .HasMaxLength(50)
                    .IsRequired();

                name.Property(n => n.LastName)
                    .HasColumnName("LastName")
                    .HasMaxLength(50)
                    .IsRequired();
            });

            // Mapeo del Value Object CandidatePhoto
            builder.OwnsOne(x => x.PhotoUrl, photo =>
            {
                photo.Property(p => p.PhotoUrl)
                    .HasColumnName("PhotoUrl")
                    .IsRequired();
            });

            builder.Property(x => x.PoliticalPartyId).IsRequired();
            builder.Property(x => x.HasParticipatedInElection).HasDefaultValue(false);
            builder.Property(x => x.State).IsRequired().HasDefaultValue(true);

            // Auditoria
            builder.Property(x => x.CreateAt).HasColumnType("datetimeoffset(0)").HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.UpdateAt).HasColumnType("datetimeoffset(0)");
            builder.Property(x => x.CreateUserId).IsRequired();

            // Índices para rendimiento y reglas de negocio
            builder.HasIndex(x => x.PoliticalPartyId);

            // Relaciones
            builder.HasOne(x => x.Partido)
                .WithMany(p => p.Candidates)
                .HasForeignKey(x => x.PoliticalPartyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
