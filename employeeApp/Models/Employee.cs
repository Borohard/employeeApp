using System;
using System.Collections.Generic;

namespace employeeApp.Models;

public partial class Employee
{
    public Guid Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? MiddleName { get; set; }
}
