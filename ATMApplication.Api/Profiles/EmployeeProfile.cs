using ATMApplication.Api.Dto;
using ATMApplication.Api.Models;
using AutoMapper;

namespace ATMApplication.Api.Profiles
{
	public class EmployeeProfile : Profile
	{
		
		public EmployeeProfile() 
		{
			CreateMap<CreateEmployeeDto, Employee>();
		}
		
	}
}
