using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class SamPreOrder
{
    public int PreOrderId { get; set; }

    public string PreOrderNo { get; set; } = null!;

    public int DepositAmount { get; set; }

    public int TotalAmount { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerAddress { get; set; }

    public string? CustomerPhone { get; set; }

    public string? Status { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public int ProductId { get; set; }

    public virtual SamProduct Product { get; set; } = null!;
}
