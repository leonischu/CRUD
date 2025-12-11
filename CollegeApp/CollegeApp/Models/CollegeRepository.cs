namespace CollegeApp.Models
{
    public class CollegeRepository
    {
        public static List<Student> Students { get; set; } = new List<Student>
        {
            new Student
            {
                Id = 1,
                StudentName = "Student 1",
                Email = "studentemail@email.com",
                Address = "KTM Nepal"
            },
            new Student
            {
                Id = 2,
                StudentName = "Student 2",
                Email = "studentemail@gmail.com",
                Address = "BKT Nepal"
            }
        };
    }
}
