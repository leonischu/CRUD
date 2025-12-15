using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Data
{
    public class CollegeDBContext : DbContext
    {
        public CollegeDBContext(DbContextOptions<CollegeDBContext> options) : base(options)
        {

        }
        DbSet<Student> Students { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(new List<Student>(){
                new Student {Id = 1,
                    StudentName = "Nischal",
                    Address="Nepal",
                    Email="Nischal@gmail.com",
                    DOB = new DateTime(2022,12,12) },
                  new Student
                  {
                      Id = 2,
                      StudentName = "Ram",
                      Address = "Nepal",
                      Email = "Ram@gmail.com",
                      DOB = new DateTime(2022, 06, 12)
                  }
            });
            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(n => n.StudentName).IsRequired();
                entity.Property(n => n.StudentName).HasMaxLength(250);
                entity.Property(n => n.Address).IsRequired(false).HasMaxLength(500);
                entity.Property(n => n.Email).IsRequired().HasMaxLength(250);
            });
        }
          
    }


    
}
