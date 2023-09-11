using System;
using System.Collections.Generic;

namespace ATM_banking_system.Models;

public partial class Atmuser
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? UserAddress { get; set; }

    public string? City { get; set; }

    public string? Email { get; set; }

    public string? Contact { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
