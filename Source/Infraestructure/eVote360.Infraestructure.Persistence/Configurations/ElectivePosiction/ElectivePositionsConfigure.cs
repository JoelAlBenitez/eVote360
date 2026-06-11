using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eVote360.Core.Domain.Entities.ElectivePosition;
namespace eVote360.Infraestructure.Persistence.Configurations.ElectivePosictions
{
    public class ElectivePositionsConfigure : IEntityTypeConfiguration<ElectivePositions>
    {
       
        public void Configure(EntityTypeBuilder<ElectivePositions> builder)
        {
            builder.ToTable("ElectivePosictions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired().UseCollation("Modern_Spanish_CI_AI");
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.State).IsRequired().HasDefaultValue(true);
            builder.Property(x => x.Description).HasMaxLength(30).IsRequired();

            builder.Property(x => x.CreateAt).
                HasColumnType("datetimeoffset(0)").
                HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.UpdateAt).
                HasColumnType("datetimeoffset(0)").
                HasDefaultValueSql("GETUTCDATE()")
                ;
            builder.Property(x => x.CreateUserId).IsRequired();


            //builder.HasOne(u => u.Users).
            //    WithMany(x => x.ElectivePosictions).
            //    HasForeignKey(u => u.CreateUserId).
            //    OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(u => u.Users).
            // WithMany(x => x.ElectivePosictions).
            // HasForeignKey(u => u.UpdateUserId).
            // OnDelete(DeleteBehavior.Restrict);

        }
    }
}
