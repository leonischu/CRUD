using AutoMapper;
using CollegeApp.Data;
using CollegeApp.Data.Repository;
using CollegeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors(PolicyName  = "AllowOnlyLocalhost")]
    [Authorize(AuthenticationSchemes = "LoginForLocalUsers",Roles = "Superadmin,Admin")] //all the endpoint inside this controller is secured 
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IMapper _mapper;
        //private readonly ICollegeRepository<Student> _studentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly APIResponse _apiResponse;
        public StudentController(ILogger<StudentController> logger,IMapper mapper,IStudentRepository studentRepository,APIResponse apiResponse)
        {
            _logger = logger;
            _mapper = mapper;
            _studentRepository = studentRepository;
            _apiResponse = apiResponse;
        }





        [HttpGet]
        [Route("All")]
        //[AllowAnonymous] // if we want to get all the student without any authentication 
        public async Task< ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
            _logger.LogInformation("Get students method started");
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

            //var students = await _dbContext.Students.ToListAsync();

            var students = await _studentRepository.GetAllAsync();
            _apiResponse.Data = _mapper.Map < List < StudentDTO >> (students);
             _apiResponse.status = true;
            _apiResponse.StatusCode = HttpStatusCode.OK;
            //{
            //    Id = s.Id,
            //    StudentName = s.StudentName,
            //    Address = s.Address,
            //    Email = s.Email,
            //    DOB = s.DOB

            //}).ToListAsync();
            return Ok(_apiResponse);    


            //var students = _dbContext.Students.ToList();

        }

        [HttpGet("{id:int}", Name = "GetStudentById")]
        public async Task<ActionResult<StudentDTO>> GetStudentByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Bad Request");
                return BadRequest();
            }
            //var student = await _dbContext.Students.Where(n => n.Id == id).FirstOrDefaultAsync();
            var student = await _studentRepository.GetAsync(student => student.Id == id);
            if (student == null)
            {
                _logger.LogError("Student not found with given Id");
                return NotFound();
            }
            //var studentDTO = new StudentDTO()
            //{
            //    Id = student.Id,
            //    StudentName = student.StudentName,
            //    Address = student.Address,
            //    Email = student.Email,
            //    DOB = student.DOB
            //};
            _apiResponse.Data = _mapper.Map< StudentDTO >(student);

            _apiResponse.status = true;
            _apiResponse.StatusCode = HttpStatusCode.OK;



            return Ok(_apiResponse);
        }


        [HttpGet]
        [Route("{name:alpha}")]

        public async Task<ActionResult<Student>> GetStudentByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name cannot be empty.");

            //var student = await _dbContext.Students
              //.FirstOrDefaultAsync(n => n.StudentName.ToLower() == name.ToLower());
              var student = await _studentRepository.GetAsync(student => student.StudentName.ToLower().Contains(name.ToLower()));

            if (student == null)
                return NotFound($"No student found with name = {name}");

            //var studentDTO = new StudentDTO()
            //{
            //    Id = student.Id,
            //    StudentName = student.StudentName,
            //    Address = student.Address,
            //    Email = student.Email,
            //    DOB = student.DOB
            //};
            _apiResponse.Data = _mapper.Map<StudentDTO>(student);


            _apiResponse.status = true;
            _apiResponse.StatusCode = HttpStatusCode.OK;


            
            return Ok(_apiResponse);


           
        }


        [HttpPost]
        [Route( "Create")]
        public async Task<ActionResult<StudentDTO>> CreateStudentAsync([FromBody ]StudentDTO dto)
        {
            if (dto == null)
                return BadRequest();

            //if(model.DOB > DateTime.Now)
            //{
            //1.Directly add the error message to model state 
            //ModelState.AddModelError("Admission Error", "Admission date must be greater tha or equal to todays date");
            //return BadRequest(ModelState);
            // 2. Using the custom attributes 



            //}


            //int newId = _dbContext.Students.LastOrDefault().Id + 1;
            //Student student = new Student {
            //    //Id = newId,
            //    StudentName = model.StudentName,
            //    Address = model.Address,
            //    Email = model.Email,
            //    DOB = model.DOB

            //};
            Student student = _mapper.Map<Student>(dto);


            //await _dbContext.Students.AddAsync(student);
           
           //await _dbContext.SaveChangesAsync();
           
            var studentAfterCreation = await _studentRepository.CreateAsync(student);

            dto.Id = studentAfterCreation.Id;

            _apiResponse.Data = dto;


            _apiResponse.status = true;
            _apiResponse.StatusCode = HttpStatusCode.OK;




            // provide Status - 201 and provide the url of newly created url,and also returns the new
            //student details i.e from models 

            return CreatedAtRoute("GetStudentById", new {id = dto.Id }, _apiResponse);

            //return Ok(model);
        }




        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult<StudentDTO>> UpdateStudentAsync([FromBody] StudentDTO dto)
        {
            if (dto == null || dto.Id <= 0)
                return BadRequest();

            //var existingStudent = await _dbContext.Students.AsNoTracking().Where(s => s.Id == dto.Id).FirstOrDefaultAsync();
             var existingStudent = await _studentRepository.GetAsync(student => student.Id == dto.Id,true);

            if (existingStudent == null)
                return NotFound();
            //var newRecord = new Student()
            //{
            //    Id = existingStudent.Id,
            //    StudentName = model.StudentName,
            //    Email = model.Email,
            //    Address = model.Address,
            //    DOB = model.DOB
            //};
            var newRecord = _mapper.Map<Student>(dto);
            //_dbContext.Students.Update(newRecord);

            await _studentRepository.UpdateAsync(newRecord);
            
            //existingStudent.StudentName = model.StudentName;
            //existingStudent.Email = model.Email;
            //existingStudent.Address = model.Address;
            //existingStudent.DOB = model.DOB;

           //await _dbContext.SaveChangesAsync();
            return NoContent();
        }




        [HttpPatch]
        [Route("{id:int}UpdatePartial")]
        public async Task<ActionResult<StudentDTO>> UpdateStudentPartialAsync(int id,[FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
                BadRequest();

            //var existingStudent = await _dbContext.Students.AsNoTracking().Where(s => s.Id ==id).FirstOrDefaultAsync();

            var existingStudent = await _studentRepository.GetAsync(student => student.Id == id, true);

            if (existingStudent == null)
                return NotFound();

            //var studentDTO = new StudentDTO
            //{
            //    Id = existingStudent.Id,
            //    StudentName = existingStudent.StudentName,
            //    Email = existingStudent.Email,
            //    Address = existingStudent.Address
            //};

            var studentDTO = _mapper.Map<StudentDTO>(existingStudent);
            patchDocument.ApplyTo(studentDTO, ModelState);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
             existingStudent = _mapper.Map<Student>(studentDTO);
           
            await _studentRepository.UpdateAsync(existingStudent);
            
            //_dbContext.Students.Update(existingStudent);

            //existingStudent.StudentName = studentDTO.StudentName;
            //existingStudent.Email = studentDTO.Email;
            //existingStudent.Address = studentDTO.Address;
            //existingStudent.DOB = studentDTO.DOB;
            //_dbContext.SaveChangesAsync();
            return NoContent();
        }



        [HttpDelete("{id}")]

        public async Task<ActionResult<bool>> DeleteStudentAsync(int id)
        {
            if (id <= 0)
                return BadRequest("ID must be greater than 0.");

            //var student = await _dbContext.Students
            //    .FirstOrDefaultAsync(n => n.Id == id);

            var student = await _studentRepository.GetAsync(student => student.Id == id);


            if (student == null)
                return NotFound($"No student found with ID = {id}");

            await _studentRepository.DeleteAsync(student);
            //_dbContext.Students.Remove(student);
            //await _dbContext.SaveChangesAsync();

            _apiResponse.Data = true;


            _apiResponse.status = true;
            _apiResponse.StatusCode = HttpStatusCode.OK;



            return Ok(_apiResponse);
        }




    }
}
