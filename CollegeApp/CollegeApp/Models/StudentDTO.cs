using CollegeApp.Validators;
using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Models
{
    public class StudentDTO
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Student name is required")]

        public string StudentName { get; set; }

        [EmailAddress(ErrorMessage = "Please enter the vaild email address")]
        public string Email { get; set; }
    
        public string Address { get; set; }
        //public int Age { get; set; }
        //[Range(10,20)]

        //public string Password { get; set; }
        //[Compare(nameof(Password))]
        //public string ConfirmPassword { get; set; }

        [DateCheck]
        public DateTime DOB { get; set; }
        
        



    }
}
