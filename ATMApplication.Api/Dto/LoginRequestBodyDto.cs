using System.ComponentModel.DataAnnotations;

namespace ATMApplication.Api.Dto
{
	
	public class LoginRequestBodyDto
	{
		
		[EmailAddress]
		[Required]
		public string Email { get; set; }
		
		
		[MinLength(1)]
		[Required]
		public string Password { get; set; }
		
		
		public LoginRequestBodyDto(string email, string password)
		{
			Email = email;
			Password = password;
		}
		
		
	}
	
	
	
}