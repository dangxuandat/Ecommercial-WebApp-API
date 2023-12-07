using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Construction
{
    public Guid Id { get; set; }

    public string Address { get; set; } = null!;

    public Guid CompanyId { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
