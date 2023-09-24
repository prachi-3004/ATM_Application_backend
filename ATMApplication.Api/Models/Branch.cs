using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ATMApplication.Api.Models
{
	
	[Index(nameof(Email), IsUnique = true)]
	public class Branch
	{
		
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		
		
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
		
		
		[MinLength(1)]
		[Required]
		public string Name { get; set; }
		
		
		public string Address { get; set; }
		
		
		[MinLength(1)]
		[Required]
		public string City { get; set; }
		
		
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		
		
		public DateTime? DeletedAt { get; set; }
		
		
		public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
		
		public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
		
		
		
		
		public Branch
		(
			string email,
			string contactNumber,
			string name,
			string address,
			string city
		)
		{
			Email = email;
			ContactNumber = contactNumber;
			Name = name;
			Address = address;
			City = city;
		}
		
		
	}
	
	
}