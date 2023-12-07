using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Company
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Construction> Constructions { get; set; } = new List<Construction>();
}
