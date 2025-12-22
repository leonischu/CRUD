using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeApp.Data.Config
{
    public class RolePrivilegeConfig : IEntityTypeConfiguration<RolePrivilege>
    {


        public void Configure(EntityTypeBuilder<RolePrivilege> builder)
        {

            builder.ToTable("RolePrivileges");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();


            builder.Property(n => n.RolePrivilegeName).IsRequired().HasMaxLength(200);
            builder.Property(n => n.Description).HasMaxLength(500).IsRequired();
            builder.Property(n => n.IsActive).HasMaxLength(500).IsRequired();
            builder.Property(n => n.IsDeleted).HasMaxLength(500).IsRequired();
            builder.Property(n => n.CreatedDate).HasMaxLength(500).IsRequired();

            builder.HasOne(n => n.Role).WithMany(n => n.RolePrivileges)
            .HasForeignKey(n => n.RoleId)
            .HasConstraintName("Fk_RolePrivileges_Roles");


        }

    }






}

