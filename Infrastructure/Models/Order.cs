using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Order
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public Guid ConstructionId { get; set; }

    public double Total { get; set; }

    public bool IsPaid { get; set; }

    public virtual Construction Construction { get; set; } = null!;
}
