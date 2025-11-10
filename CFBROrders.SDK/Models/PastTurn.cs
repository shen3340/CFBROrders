using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
public partial class PastTurn
{
    [Column("id")]
    public int? Id { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("territory")]
    public int? Territory { get; set; }

    [Column("mvp")]
    public bool? Mvp { get; set; }

    [Column("power")]
    public double? Power { get; set; }

    [Column("multiplier")]
    public double? Multiplier { get; set; }

    [Column("weight")]
    public double? Weight { get; set; }

    [Column("stars")]
    public int? Stars { get; set; }

    [Column("team")]
    public int? Team { get; set; }

    [Column("alt_score")]
    public int? AltScore { get; set; }

    [Column("merc")]
    public bool? Merc { get; set; }

    [Column("turn_id")]
    public int? TurnId { get; set; }
}
