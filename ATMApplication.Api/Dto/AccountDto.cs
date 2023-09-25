using System.ComponentModel.DataAnnotations;
using ATMApplication.Api.Enums;

namespace ATMApplication.Api.Dto
{

	public partial class AccountDto
	{
		
		[Required]
		public int CustomerId { get; set; }
		
		[Required]
		public int Balance { get; set; }

		[Required]
		public string Pin { get; set; }

		public AccountDto
		(
			int customerId,
			int balance,
			string pin
		)
		{
			CustomerId = customerId;
			Balance = balance;
			Pin = pin;
		}
		
	}
	
}
