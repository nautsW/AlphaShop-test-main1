﻿using System;
using System.Collections.Generic;

namespace AlphaShop.Data;

public partial class Customer
{
    public int CtrId { get; set; }

    public string CtrLogusername { get; set; } = null!;

    public string? CtrUsername { get; set; }

    public string? CtrPassword { get; set; }

    public bool? CtrGender { get; set; }

    public string? CtrEmail { get; set; }

    public string? CtrPhonenumber { get; set; }

    public int CtrAccess { get; set; }

    public decimal? CtrUsed { get; set; }

    public string? CtrImage { get; set; }

    public string? CtrAddress { get; set; }

    public int? CtrStatus { get; set; }

    public bool? CtrVisible { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual Cart? Cart { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Access CtrAccessNavigation { get; set; } = null!;

    public virtual ICollection<Ord> Ords { get; set; } = new List<Ord>();
}
