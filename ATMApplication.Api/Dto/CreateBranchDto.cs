using System.ComponentModel.DataAnnotations;
using ATMApplication.Api.Models;

namespace ATMApplication.Api.Dto
{
	
	public class CreateBranchDto
	{
		
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		
		
		[Required]
		public string ContactNumber { get; set; }
		
		
		[MinLength(1)]
		[Required]
		public string Name { get; set; }
		
		
		[MinLength(1)]
		[Required]
		public string City { get; set; }
		
		
		public CreateBranchDto
		(
			string email, 
			string contactNumber, 
			string name, 
			string city
		)
		{
			Email = email;
			ContactNumber = contactNumber;
			Name = name;
			City = city;
		}
		
	}
	
	
}