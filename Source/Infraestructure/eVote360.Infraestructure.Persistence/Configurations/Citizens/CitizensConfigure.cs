using eVote360.Core.Domain.Entities.Citizens;
using eVote360.Core.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote360.Infraestructure.Persistence.Configurations.Citizens
{
    public class CitizensConfigure : IEntityTypeConfiguration<Citizen>
    {
        public void Configure(EntityTypeBuilder<Citizen> builder)
        {
            builder.ToTable("Citizens");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.Name).HasMaxLength(40).IsRequired();

            builder.Property(x => x.LastName).HasMaxLength(40).IsRequired();

            builder.OwnsOne(x => x.Email, email =>
            {
                email.Property(e => e.Value)
                    .HasColumnName("Email")
                    .HasColumnType("nvarchar")
                    .HasMaxLength(254)
                    .IsRequired();
                email.HasIndex(e => e.Value).IsUnique();
            });

            builder.OwnsOne(x => x.IdentificationNumber, id =>
            {
                id.Property(i => i.Value)
                    .HasColumnName("IdentificationNumber")
                    .HasColumnType("nvarchar")
                    .HasMaxLength(11)
                    .IsRequired();
                id.HasIndex(i => i.Value).IsUnique();
            });

            builder.Property(x => x.State).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.CreateAt).HasDefaultValueSql("SYSDATETIMEOFFSET()");

          
            builder.HasOne(u => u.UserCreate)
                .WithMany()
                .HasForeignKey(u => u.CreateUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.UserUpdate)
               .WithMany()
               .HasForeignKey(u => u.UpdateUserId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
