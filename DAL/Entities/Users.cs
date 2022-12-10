using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Users
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public int? No { get; set; }

    public string? Password { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<DocumentStatistics> DocumentStatistics { get; } = new List<DocumentStatistics>();
}
