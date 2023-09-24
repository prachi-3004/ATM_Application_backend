using ATMApplication.Api.Dto;
using ATMApplication.Api.Models;
using AutoMapper;

namespace ATMApplication.Api.Profiles
{
	public class CustomerProfile : Profile
	{
		
		public CustomerProfile() 
		{
			CreateMap<CustomerDto, Customer>();
		}
		
	}
}
