using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeApp.Data.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {


        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.ToTable("User");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();


            builder.Property(n => n.Username).IsRequired().HasMaxLength(200);
            builder.Property(n => n.Password).HasMaxLength(500).IsRequired();
            builder.Property(n => n.PasswordSalt).HasMaxLength(500).IsRequired();
            builder.Property(n => n.IsActive).HasMaxLength(500).IsRequired();
            builder.Property(n => n.IsDeleted).HasMaxLength(500).IsRequired();
            builder.Property(n => n.CreatedDate).HasMaxLength(500).IsRequired();
            builder.Property(n => n.UserTypeId).HasMaxLength(500).IsRequired();


            builder.HasOne(n => n.UserType).WithMany(n => n.Users)
                .HasForeignKey(n => n.UserTypeId)
                .HasConstraintName("Fk_Users_UserTypes");



        }
    }
       
}
