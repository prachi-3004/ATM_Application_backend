using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ATMApplication.Api.Enums;

namespace ATMApplication.Api.Models
{
	
	
	public class Card
	{
		
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		
		
		[Required]
		public string Pin { get; set; }
		
		
		[ForeignKey("AccountId")]
		public virtual Account Account { get; set; } = null!;
		public int AccountId { get; set; }
		
		
		public CardType Type { get; set; }
		
		
		public CardStatus Status { get; set; } = CardStatus.ACTIVE;
		
		
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		
		
		public DateTime? DeletedAt { get; set; }
		
		
		public Card
		(
			string pin,
			int accountId,
			CardType type,
			CardStatus status
		)
		{
			Pin = pin;
			AccountId = accountId;
			Type = type;
			Status = status;
		}
		
		
	}
	
	
	
}