using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeApp.Data
{
    public class Student
    {
        //[Key] //It indicates the Primary Key 
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Generates the value of a column i.e DB automatically generates the value for a primary key.
        public int Id { get; set; }
        public string StudentName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public DateTime DOB { get; set; }

    }
}
