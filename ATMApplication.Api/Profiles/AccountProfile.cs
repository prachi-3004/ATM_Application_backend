using ATMApplication.Api.Dto;
using ATMApplication.Api.Models;
using AutoMapper;

namespace ATMApplication.Api.Profiles
{
	public class AccountProfile : Profile
	{

		public AccountProfile() 
		{
			CreateMap<AccountDto, Account>();
		}
		
	}
}
