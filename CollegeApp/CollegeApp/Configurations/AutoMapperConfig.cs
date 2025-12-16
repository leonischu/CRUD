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

            //Consiguration for different property names
            //CreateMap<StudentDTO, Student>().ForMember(n => n.StudentName, opt => opt.MapFrom(x => x.Name)).ReverseMap();

            //Configuration for ignoring some property
            //CreateMap<StudentDTO, Student>().ReverseMap().ForMember(n => n.StudentName, opt =>opt.Ignore());

            //configuration for transforming some values.to display some meaningful value instead of null.like no data found use this property.
            //CreateMap<StudentDTO, Student>().ReverseMap().AddTransform<string>(n => string.IsNullOrEmpty(n)? "No address found" : n);
            CreateMap<StudentDTO, Student>().ReverseMap();


        }
    }
}
