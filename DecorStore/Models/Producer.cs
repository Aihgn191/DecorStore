using System;
using System.Collections.Generic;

namespace DecorStore.Models;

public partial class Producer
{
    public int IdProducer { get; set; }

    public string? ProducerName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNums { get; set; }

    public string? Address { get; set; }

    public string? Img { get; set; }

    public int? Hide { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
