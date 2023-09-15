using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ATM_banking_system.Models;

public partial class Transaction
{
    //[Key]
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //public int Id { get; set; }

    public int TransactionId { get; set; }

    public string? TransactionType { get; set; }

    public int? AccountId { get; set; }

    public DateTime? DateOfTransaction { get; set; }

    public int? Amount { get; set; }

    public virtual Account? Account { get; set; }
}
