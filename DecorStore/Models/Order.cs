using System;
using System.Collections.Generic;

namespace DecorStore.Models;

public partial class Order
{
    public int IdOrder { get; set; }

    public string IdUser { get; set; } = null!;

    public DateTime? OrderDate { get; set; }

    public string? Address { get; set; }

    public decimal? TotalPrice { get; set; }

    public string? Notes { get; set; }

    public bool? IsPay { get; set; }

    public bool? IsComplete { get; set; }

    public string? PtThanhtoan { get; set; }

    public virtual UserCustom? UserCustom {  get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
