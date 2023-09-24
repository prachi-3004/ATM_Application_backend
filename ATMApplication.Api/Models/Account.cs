using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ATMApplication.Api.Enums;

namespace ATMApplication.Api.Models
{
	
	public partial class Account
	{
		
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		
		
		public AccountType Type { get; set; } = AccountType.Savings;
		
		
		public AccountStatus Status { get; set; } = AccountStatus.ACTIVE;
		
		
		[ForeignKey("CustomerId")]
		public virtual Customer Customer { get; set; } = null!;
		public int CustomerId { get; set; }
		
		
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		
		
		public DateTime? DeletedAt { get; set; }
		
		
		[Required]
		public int Amount { get; set; }
		
		
		public CurrencyType Currency { get; set; } = CurrencyType.INR;
		
		
		public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
		
		public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
		
		
		
		public Account
		(
			int customerId,
			int amount
		)
		{
			CustomerId = customerId;
			Amount = amount;
		}
		
		
	}
	
	
	
}