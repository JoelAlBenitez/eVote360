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
            builder.Property(x => x.Email.Value).HasColumnType("nvarchar").HasMaxLength(254).IsRequired();

            builder.Property(x => x.IdentificationNumber.Value).HasColumnType("nvarchar").HasMaxLength(11).IsRequired();

            builder.Property(x => x.State).IsRequired().HasDefaultValue(true);

            builder.HasIndex(x => x.Email.Value).IsUnique();
            builder.HasIndex(x => x.IdentificationNumber.Value).IsUnique();

            builder.Property(x => x.CreateAt).HasDefaultValue("SYSDATETIMEOFFSET()");

          
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
