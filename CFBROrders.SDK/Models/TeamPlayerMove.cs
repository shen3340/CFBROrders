using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
public partial class TeamPlayerMove
{
    [Column("id")]
    public int? Id { get; set; }

    [Column("season")]
    public int? Season { get; set; }

    [Column("day")]
    public int? Day { get; set; }

    [Column("team", TypeName = "citext")]
    public string? Team { get; set; }

    [Column("player", TypeName = "citext")]
    public string? Player { get; set; }

    [Column("stars")]
    public int? Stars { get; set; }

    [Column("mvp")]
    public bool? Mvp { get; set; }

    [Column("territory", TypeName = "citext")]
    public string? Territory { get; set; }

    [Column("regularteam", TypeName = "citext")]
    public string? Regularteam { get; set; }

    [Column("weight")]
    public double? Weight { get; set; }

    [Column("power")]
    public double? Power { get; set; }

    [Column("multiplier")]
    public double? Multiplier { get; set; }
}
