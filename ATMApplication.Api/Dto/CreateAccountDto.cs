using System.ComponentModel.DataAnnotations;
using ATMApplication.Api.Enums;

namespace ATMApplication.Api.Dto
{
	public partial class CreateAccountDto
	{
		
		
		[Required]
		public int CustomerId { get; set; }
		
		
		
		[Required]
		public int Amount { get; set; }
		
		
		
		public CreateAccountDto
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
