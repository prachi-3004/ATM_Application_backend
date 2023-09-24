using ATMApplication.Api.Dto;
using ATMApplication.Api.Models;
using AutoMapper;

namespace ATMApplication.Api.Profiles
{
	public class BranchProfile : Profile
	{

		public BranchProfile() 
		{
			CreateMap<CreateBranchDto, Branch>();
		}

	}
}
