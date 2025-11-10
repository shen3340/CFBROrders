using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
[Table("territory_stats")]
public partial class TerritoryStat
{
    [Column("team")]
    public int? Team { get; set; }

    [Column("ones")]
    public int? Ones { get; set; }

    [Column("twos")]
    public int? Twos { get; set; }

    [Column("threes")]
    public int? Threes { get; set; }

    [Column("fours")]
    public int? Fours { get; set; }

    [Column("fives")]
    public int? Fives { get; set; }

    [Column("teampower")]
    public double? Teampower { get; set; }

    [Column("chance")]
    public double? Chance { get; set; }

    [Column("id")]
    public int Id { get; set; }

    [Column("territory")]
    public int? Territory { get; set; }

    [Column("territory_power")]
    public double? TerritoryPower { get; set; }

    [Column("turn_id")]
    public int? TurnId { get; set; }
}
