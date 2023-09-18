using System;
using System.Collections.Generic;

namespace ATM.Models;

public partial class Account
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public string? Type { get; set; }

    public DateTime? DateOfCreation { get; set; }

    public string? CardNumber { get; set; }

    public string? Pin { get; set; }

    public int? Balance { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
