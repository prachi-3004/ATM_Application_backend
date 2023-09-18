using System;
using System.Collections.Generic;

namespace ATM.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public int? AccountId { get; set; }

    public DateTime? Time { get; set; }

    public int? LinkedId { get; set; }

    public int? Amount { get; set; }

    public virtual Account? Account { get; set; }
}
