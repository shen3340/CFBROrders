using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
public partial class Move
{
    [Column("season")]
    public int? Season { get; set; }

    [Column("day")]
    public int? Day { get; set; }

    [Column("territory")]
    public int? Territory { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("team")]
    public int? Team { get; set; }

    [Column("player")]
    public int? Player { get; set; }

    [Column("mvp")]
    public bool? Mvp { get; set; }

    [Column("uname", TypeName = "citext")]
    public string? Uname { get; set; }

    [Column("turns")]
    public int? Turns { get; set; }

    [Column("mvps")]
    public int? Mvps { get; set; }

    [Column("tname", TypeName = "citext")]
    public string? Tname { get; set; }

    [Column("power")]
    public double? Power { get; set; }

    [Column("weight")]
    public double? Weight { get; set; }

    [Column("stars")]
    public int? Stars { get; set; }

    [Column("current_stars")]
    public int? CurrentStars { get; set; }
}
