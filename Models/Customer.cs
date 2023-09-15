using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ATM_banking_system.Models;

public partial class Customer
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "Name is a required parameter")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is a required parameter")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Address is a required parameter")]
    public string? UserAddress { get; set; }

    [Required(ErrorMessage = "City is a required parameter")]
    public string? City { get; set; }

    [EmailAddress(ErrorMessage = "Enter a valid Email Address")]
    [Required(ErrorMessage = "Email is a required parameter")]
    public string? Email { get; set; }

    public string? Contact { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
