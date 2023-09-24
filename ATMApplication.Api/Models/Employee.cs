using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ATMApplication.Api.Enums;
using Microsoft.EntityFrameworkCore;

namespace ATMApplication.Api.Models
{
	
	[Index(nameof(Email), IsUnique = true)]
	public class Employee
	{
		
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		
		
		[Required]
		public string GovernmentId { get; set; }
		
		
		public EmployeeRole Role { get; set; }
		
		
		private string _email = "";
		
		
		[Required]
		[EmailAddress]
		public string Email
		{
			get { return _email; }
			set { _email = value.ToLower(); }
		}
		
		
		[Required]
		public string ContactNumber { get; set; }
		
		
		[MinLength(1)]
		[Required]
		public string Password { get; set; }
		
		
		[ForeignKey("BranchId")]
		public virtual Branch? Branch { get; set; }
		public int BranchId { get; set; }
		
		
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		
		
		public DateTime? DeletedAt { get; set; }
		
		
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
		
		
		[Required]
		public DateTime DateOfBirth { get; set; }

		
		public Employee
		(
			string governmentId, 
			string email, 
			string contactNumber, 
			string password, 
			string name, 
			string address, 
			string city,
			EmployeeStatus status,
			int branchId,
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
			BranchId = branchId;
			DateOfBirth = dateOfBirth;
		}
		
		
	}
	
	
}