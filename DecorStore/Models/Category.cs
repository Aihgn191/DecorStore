using System;
using System.Collections.Generic;

namespace DecorStore.Models;

public partial class Category
{
    public int IdCate { get; set; }

    public string CateName { get; set; } = null!;

    public string? Link { get; set; }

    public int? Hide { get; set; }

    public string? Description { get; set; }

    public string? Img { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
