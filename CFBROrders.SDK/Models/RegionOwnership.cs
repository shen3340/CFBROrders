using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
public partial class RegionOwnership
{
    [Column("owner_count")]
    public long? OwnerCount { get; set; }

    [Column("owners")]
    public List<int>? Owners { get; set; }

    [Column("day")]
    public int? Day { get; set; }

    [Column("season")]
    public int? Season { get; set; }

    [Column("region")]
    public int? Region { get; set; }
}
