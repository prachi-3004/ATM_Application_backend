using System;
using System.Collections.Generic;

namespace ATM_banking_system.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public int? UserId { get; set; }

    public string? AccountType { get; set; }

    public DateTime? DateOfCreation { get; set; }

    public string? CardNo { get; set; }

    public string? PinNo { get; set; }

    public int? Balance { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual Customer? User { get; set; }
}
