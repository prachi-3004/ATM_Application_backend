using System.ComponentModel.DataAnnotations;
using ATMApplication.Api.Enums;
using ATMApplication.Api.Models;

namespace ATMApplication.Api.Dto
{
	
	public class CreateEmployeeDto
	{
		
		
		[Required]
		public string GovernmentId { get; set; }
		
		
		public EmployeeRole Role { get; set; }
		
		
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		
		
		[Required]
		public string ContactNumber { get; set; }
		
		
		[Required]
		public string Password { get; set; }
		
		
		[Required]
		public int BranchId { get; set; }
		
		
		public EmployeeStatus Status { get; set; }
		
		
		[MinLength(1)]
		[Required]
		public string Name { get; set; }
		
		
		[MinLength(1)]
		[Required]
		public string Address { get; set; }
		
		
		[MinLength(1)]
		[Required]
		public string City { get; set; }
		
		
		public DateTime DateOfBirth { get; set; }
		
		
		public CreateEmployeeDto
		(
			string governmentId, 
			string email, 
			string contactNumber, 
			string password, 
			string name, 
			string address, 
			string city,
			EmployeeStatus status,
			DateTime dateOfBirth
		)
		{
			GovernmentId = governmentId;
			Email = email;
			ContactNumber = contactNumber;
			Password = password;
			Name = name;
			Address = address;
			City = city;
			Status = status;
			DateOfBirth = dateOfBirth;
		}
		
		
		
	}
	
	
}