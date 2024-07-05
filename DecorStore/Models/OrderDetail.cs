using System;
using System.Collections.Generic;

namespace DecorStore.Models;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int IdOrder { get; set; }

    public int IdProd { get; set; }

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public decimal? Sale { get; set; }

    public virtual Product IdOrder1 { get; set; } = null!;

    public virtual Order IdOrderNavigation { get; set; } = null!;
}
