using Practical_18_Core.Models;
using AutoMapper;


namespace Practical_18_Core.Profiles
{

    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentDto>();
            CreateMap<CreateStudentDto, Student>();
            CreateMap<UpdateStudentDto, Student>();
            CreateMap<Student, UpdateStudentDto>();
        }
    }

}
