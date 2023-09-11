using System;
using System.Collections.Generic;

namespace ATM_banking_system.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public string? TransactionType { get; set; }

    public int? AccountId { get; set; }

    public DateTime? DateOfTransaction { get; set; }

    public int? Amount { get; set; }

    public virtual Account? Account { get; set; }
}
