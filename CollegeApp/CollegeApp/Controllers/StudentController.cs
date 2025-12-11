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
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            return Ok(CollegeRepository.Students);    
        }

        [HttpGet("{id:int}")]
        public ActionResult<Student> GetStudentById(int id)
        {
            if(id <= 0) 

                return BadRequest();
            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            if (student == null)
                return NotFound();
            
            return Ok(student);
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

            return Ok(student);
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
