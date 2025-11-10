using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
public partial class TerritoryOwnershipWithoutNeighbor
{
    [Column("territory_id")]
    public int? TerritoryId { get; set; }

    [Column("day")]
    public int? Day { get; set; }

    [Column("season")]
    public int? Season { get; set; }

    [Column("name", TypeName = "citext")]
    public string? Name { get; set; }

    [Column("owner", TypeName = "citext")]
    public string? Owner { get; set; }

    [Column("prev_owner", TypeName = "citext")]
    public string? PrevOwner { get; set; }

    [Column("timestamp", TypeName = "timestamp without time zone")]
    public DateTime? Timestamp { get; set; }

    [Column("random_number")]
    public double? RandomNumber { get; set; }

    [Column("mvp", TypeName = "citext")]
    public string? Mvp { get; set; }
}
