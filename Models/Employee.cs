using System;
using System.Collections.Generic;

namespace ATM.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }
}
