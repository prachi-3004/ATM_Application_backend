using System.ComponentModel.DataAnnotations;
using ATMApplication.Api.Enums;

namespace ATMApplication.Api.Dto
{
	
	public class CustomerDto
	{
		
		[Required]
		public string GovernmentId { get; set; }
		
		[EmailAddress]
		[Required]
		public string Email { get; set; }
		
		[Required]
		public string ContactNumber { get; set; }
		
		[MinLength(1)]
		[Required]
		public string Password { get; set; }
		
		[Required]
		public string Name { get; set; }
		
		[Required]
		public string Address { get; set; }
		
		[Required]
		public string City { get; set; }
		
		public CustomerDto
		(
			string governmentId, 
			string email, 
			string contactNumber, 
			string password, 
			string name, 
			string address, 
			string city
		)
		{
			GovernmentId = governmentId;
			Email = email;
			ContactNumber = contactNumber;
			Password = password;
			Name = name;
			Address = address;
			City = city;
		}
		
		
		
	}
	
	
	
}