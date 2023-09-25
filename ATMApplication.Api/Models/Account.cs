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
		
		public AccountType Type { get; set; }
		
		public AccountStatus Status { get; set; }
		
		[ForeignKey("CustomerId")]
		public virtual Customer Customer { get; set; }

		public int CustomerId { get; set; }
		
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		
		public DateTime? DeletedAt { get; set; }
		
		[Required]
		public int Balance { get; set; }

		[Required]
		public string Pin { get; set; }

		public CurrencyType Currency { get; set; } = CurrencyType.INR;
		
		public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
		
		public Account
		(
			int customerId,
			int balance,
			string pin,
			AccountType type=AccountType.Savings,
			AccountStatus status=AccountStatus.ACTIVE,
			CurrencyType currency=CurrencyType.INR
        )
		{
			CustomerId = customerId;
			Balance = balance;
			Pin = pin;
			Type = type;
			Status = status;
			Currency = currency;
		}
		
	}

}