using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Models
{
    public class LoginDTO
    {
        public string Policy {  get; set; } 
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
