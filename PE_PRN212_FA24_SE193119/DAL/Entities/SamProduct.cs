using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class SamProduct
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int Ram { get; set; }

    public int Storage { get; set; }

    public int Price { get; set; }

    public DateOnly CreatedAt { get; set; }

    public DateOnly UpdatedAt { get; set; }

    public virtual ICollection<SamPreOrder> SamPreOrders { get; set; } = new List<SamPreOrder>();
}
