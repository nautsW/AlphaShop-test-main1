using System;
using System.Collections.Generic;

namespace AlphaShop.Data;

public partial class Address
{
    public int CtrId { get; set; }

    public int AddId { get; set; }

    public string AddressName { get; set; } = null!;

    public virtual Customer Ctr { get; set; } = null!;
}
