using System;
using System.Collections.Generic;

namespace DecorStore.Models;

public partial class Favorite
{
    public int IdFavo { get; set; }

    public string IdUser { get; set; } = null!;

    public int IdProd { get; set; }

    public DateTime? Date { get; set; }
}
