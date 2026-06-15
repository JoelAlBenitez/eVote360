using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CandidateAssignmentEntity = eVote360.Core.Domain.Entities.CandidateAssignment.CandidateAssignment;

namespace eVote360.Infraestructure.Persistence.Configurations.CandidateAssignment
{
    public class CandidateAssignmentConfigure : IEntityTypeConfiguration<CandidateAssignmentEntity>
    {
        public void Configure(EntityTypeBuilder<CandidateAssignmentEntity> builder)
        {
            builder.ToTable("CandidateAssignments");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.AssigningPartyId).IsRequired();
            builder.Property(x => x.CandidateId).IsRequired();
            builder.Property(x => x.ElectivePositionId).IsRequired();

            builder.Property(x => x.CreateAt)
                .HasColumnType("datetimeoffset(0)")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.CreateUserId).IsRequired();

            // Regla: Un partido no puede tener más de un candidato por puesto
            builder.HasIndex(x => new { x.AssigningPartyId, x.ElectivePositionId }).IsUnique();

            // Regla: Un candidato no puede estar en más de un puesto dentro del mismo partido
            builder.HasIndex(x => new { x.AssigningPartyId, x.CandidateId }).IsUnique();

            // Relaciones
            builder.HasOne(x => x.Candidate)
                .WithMany(c => c.AsignacionesPuestos)
                .HasForeignKey(x => x.CandidateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ElectivePosition)
                .WithMany()
                .HasForeignKey(x => x.ElectivePositionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
