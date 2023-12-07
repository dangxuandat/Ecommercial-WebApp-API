using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Supply
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}
