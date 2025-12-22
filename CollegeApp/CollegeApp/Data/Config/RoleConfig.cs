using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeApp.Data.Config
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {


        public void Configure(EntityTypeBuilder<Role> builder)
        {

            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();


            builder.Property(n => n.RoleName).IsRequired().HasMaxLength(200);
            builder.Property(n => n.Description).HasMaxLength(500).IsRequired();
            builder.Property(n => n.IsActive).HasMaxLength(500).IsRequired();
            builder.Property(n => n.IsDeleted).HasMaxLength(500).IsRequired();
            builder.Property(n => n.CreatedDate).HasMaxLength(500).IsRequired();

        }
    
    }
}
