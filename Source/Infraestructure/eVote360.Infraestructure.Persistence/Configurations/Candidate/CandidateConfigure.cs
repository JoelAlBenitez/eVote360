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
        }
    }
}
