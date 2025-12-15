using AutoMapper;
using CollegeApp.Data;
using CollegeApp.Models;

namespace CollegeApp.Configurations
{
    public class AutoMapperConfig : Profile
    {

        public AutoMapperConfig() {

            //CreateMap<Student, StudentDTO>();
            //CreateMap<StudentDTO, Student>();
            //Instead of using this two we can use reverse map

            CreateMap<StudentDTO, Student>().ReverseMap();
        }
    }
}
