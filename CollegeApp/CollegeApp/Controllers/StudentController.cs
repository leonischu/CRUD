using CollegeApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public class StudentController : ControllerBase
    {

        [HttpGet]
        [Route("All")]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
            //var students = new List<StudentDTO>();
            //foreach (var item in CollegeRepository.Students)
            //{
            //    StudentDTO obj = new StudentDTO()
            //    {
            //        Id = item.Id,
            //        StudentName = item.StudentName,
            //        Address = item.Address,
            //        Email = item.Email
            //    };
            //    students.Add(obj);
            //}

            var students = CollegeRepository.Students.Select(s => new StudentDTO()

            {
                Id = s.Id,
                StudentName = s.StudentName,
                Address = s.Address,
                Email = s.Email
            });
            return Ok(students);    
        }

        [HttpGet("{id:int}")]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            if(id <= 0) 

                return BadRequest();
            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            if (student == null)
                return NotFound();
            var studentDTO = new StudentDTO()
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Address = student.Address,
                Email = student.Email
            };
            return Ok(studentDTO);
        }


        [HttpGet]
        [Route("{name:alpha}")]

        public ActionResult <Student> GetStudentByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name cannot be empty.");

            var student = CollegeRepository.Students
                .FirstOrDefault(n => n.StudentName.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (student == null)
                return NotFound($"No student found with name = {name}");

            var studentDTO = new StudentDTO()
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Address = student.Address,
                Email = student.Email
            };
            return Ok(studentDTO);


           
        }



        [HttpDelete("{id}")]

        public ActionResult<bool> DeleteStudent(int id)
        {
            if (id <= 0)
                return BadRequest("ID must be greater than 0.");

            var student = CollegeRepository.Students
                .FirstOrDefault(n => n.Id == id);

            if (student == null)
                return NotFound($"No student found with ID = {id}");

            CollegeRepository.Students.Remove(student);

            return Ok(true);
        }




    }
}
