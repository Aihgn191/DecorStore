using System;
using System.Collections.Generic;

namespace DecorStore.Models;

public partial class Product
{
    public int IdProd { get; set; }

    public string ProdName { get; set; } = null!;

    public string? AliasName { get; set; }

    public int IdCate { get; set; }

    public int IdProducer { get; set; }

    public string? Unit { get; set; }

    public decimal Price { get; set; }

    public string? Img1 { get; set; }

    public string? Img2 { get; set; }

    public string? Img3 { get; set; }

    public DateTime? Nsx { get; set; }

    public decimal? Sale { get; set; }

    public string? Description { get; set; }

    public string? Link { get; set; }

    public int? Hide { get; set; }

    public int? Nums { get; set; }

    public virtual Category IdCateNavigation { get; set; } = null!;

    public virtual Producer IdProducerNavigation { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
