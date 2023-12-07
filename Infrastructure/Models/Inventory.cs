using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Inventory
{
    public Guid Id { get; set; }

    public double Price { get; set; }

    public double Quantity { get; set; }

    public Guid ProductId { get; set; }

    public Guid SupplyId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Supply Supply { get; set; } = null!;
}
