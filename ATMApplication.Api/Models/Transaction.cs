using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ATMApplication.Api.Enums;

namespace ATMApplication.Api.Models
{
	
	public partial class Transaction
	{
		
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		
		public int? TransactionId { get; set; }
		
		[Required]
		public TransactionType? Type { get; set; }

		public virtual Account? Account { get; set; }
		[ForeignKey("AccountId")]
		public int AccountId { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		
		[Required]
		public int Amount { get; set; }
		
		public CurrencyType Currency { get; set; } = CurrencyType.INR;
		
		public string Description { get; set; }
		
		public Transaction
		(
			TransactionType? type,
			int accountId,
			int amount,
			string description = ""
		)
		{
			Type = type;
			AccountId = accountId;
			Amount = amount;
			Description = description;
		}
		
	}
	
}