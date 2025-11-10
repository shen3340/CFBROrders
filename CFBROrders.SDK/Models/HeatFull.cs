using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
public partial class HeatFull
{
    [Column("name", TypeName = "citext")]
    public string? Name { get; set; }

    [Column("season")]
    public int? Season { get; set; }

    [Column("day")]
    public int? Day { get; set; }

    [Column("cumulative_players")]
    public long? CumulativePlayers { get; set; }

    [Column("cumulative_power")]
    public double? CumulativePower { get; set; }

    [Column("owner", TypeName = "citext")]
    public string? Owner { get; set; }
}
