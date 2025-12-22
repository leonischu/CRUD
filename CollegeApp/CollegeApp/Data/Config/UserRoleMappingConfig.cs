using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeApp.Data.Config
{
    public class UserRoleMappingConfig : IEntityTypeConfiguration<UseRoleMapping>
    {


        public void Configure(EntityTypeBuilder<UseRoleMapping> builder)
        {

            builder.ToTable("UserRoleMappings");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasIndex(n => new { n.UserId, n.RoleId }, "UK_UserRoleMapping").IsUnique();    //Combination of this key is unique key

            builder.HasOne(n => n.Role).WithMany(n => n.UserRoleMappings)
           .HasForeignKey(n => n.RoleId)
           .HasConstraintName("Fk_UserRoleMapping_Role");

            builder.HasOne(n => n.User).WithMany(n => n.UserRoleMappings)
        .HasForeignKey(n => n.UserId)
        .HasConstraintName("Fk_UserRoleMapping_User");

        }
    }
    
    
}
