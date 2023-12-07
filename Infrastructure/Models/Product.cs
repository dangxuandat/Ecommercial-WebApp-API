using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Product
{
    public string Name { get; set; } = null!;

    public Guid Id { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}
