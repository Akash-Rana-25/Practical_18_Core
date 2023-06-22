using AutoMapper;
using Practical_18_API.Entities;
using Practical_18_API.Models;

namespace Practical_18_API.Profiles;

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
