using AutoMapper;
using Practical18_MVC.ViewModels;

namespace Practical18_MVC.Profiles;

public class StudentProfile : Profile
{
	public StudentProfile()
	{
		CreateMap<StudentViewModel, EditStudentViewModel>();
	}
}
