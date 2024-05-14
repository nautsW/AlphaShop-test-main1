using System;
using System.Collections.Generic;

namespace AlphaShop.Data;

public partial class Ord
{
    public int OrdId { get; set; }

    public int CartId { get; set; }

    public int StaffId { get; set; }

    public int? OrdStatus { get; set; }

    public string? OrdDest { get; set; }

    public decimal? OrdPrice { get; set; }

    public DateTime? OrdDate { get; set; }

    public string? OrdNote { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual ICollection<OrdDetail> OrdDetails { get; set; } = new List<OrdDetail>();

    public virtual Customer Staff { get; set; } = null!;
}
