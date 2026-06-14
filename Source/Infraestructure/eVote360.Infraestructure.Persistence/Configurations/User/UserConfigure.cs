using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserEntity = eVote360.Core.Domain.Entities.User.User;

namespace eVote360.Infraestructure.Persistence.Configurations.User
{
    public class UserConfigure : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder) {
        
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);

            builder.Property(x  => x.Id)
                .UseIdentityColumn(1,1);


                builder.Property(x => x.Name)
                   .HasColumnName("Username")
                   .HasMaxLength(50)
                   .HasColumnType("nvarchar")
                   .IsRequired();
            
                builder.Property(x => x.UserFirstName)
                   .HasColumnName("FirstName")
                   .HasMaxLength(50)
                   .HasColumnType("nvarchar")
                   .IsRequired();
            
                builder.Property(x => x.UserLastName)
                   .HasColumnName("LastName")
                   .HasMaxLength(50)
                   .HasColumnType("nvarchar")
                   .IsRequired();
            
                builder.Property(x => x.UserEmail.Value)
                   .HasColumnName("Email")
                   .HasMaxLength(100)
                   .HasColumnType("nvarchar")
                   .IsRequired();

                builder.Property(x => x.UserPassword.HashValue)
                   .IsRequired();
            
                 builder.Property(x => x.UserRole)
                   .HasConversion<string>() 
                   .IsRequired();
            
                builder.Property(x => x.State)
                   .IsRequired()
                   .HasDefaultValue(true);
                
                builder.HasIndex(x => x.Name).IsUnique();
                         builder.HasIndex(x => x.UserEmail.Value).IsUnique();
        }
    }

}

