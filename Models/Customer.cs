using System;
using System.Collections.Generic;

namespace ATM.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string? Address { get; set; }

    public string? City { get; set; }

    public string Email { get; set; } = null!;

    public string? Contact { get; set; }

    public string Password { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
