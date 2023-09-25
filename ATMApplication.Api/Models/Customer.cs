using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ATMApplication.Api.Enums;
using Microsoft.EntityFrameworkCore;

namespace ATMApplication.Api.Models
{
	
	[Index(nameof(Email), IsUnique = true)]
	public class Customer
	{
		
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		
		
		[Required]
		public string GovernmentId { get; set; }
		
		
		private string _email = null!;
		
		[Required]
		[EmailAddress]
		public string Email
		{
			get { return _email; }
			set { _email = value.ToLower(); }
		}
		
		[Required]
		public string ContactNumber { get; set; }
		
		
		public string Password { get; set; }
		
		
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		
		
		public DateTime? DeletedAt { get; set; }


		public CustomerStatus Status { get; set; } = CustomerStatus.Active;
		
		
		[MinLength(1)]
		[Required]
		public string Name { get; set; }
		
		
		[MinLength(1)]
		[Required]
		public string Address { get; set; }
		
		
		[MinLength(1)]
		[Required]
		public string City { get; set; }
		
		
		public DateTime? DateOfBirth { get; set; }
		
		
		public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
		
		
		
		public Customer
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