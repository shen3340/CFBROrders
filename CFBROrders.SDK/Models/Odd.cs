using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
public partial class Odd
{
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

    [Column("players")]
    public int? Players { get; set; }

    [Column("teampower")]
    public double? Teampower { get; set; }

    [Column("territorypower")]
    public double? Territorypower { get; set; }

    [Column("chance")]
    public double? Chance { get; set; }

    [Column("team")]
    public int? Team { get; set; }

    [Column("season")]
    public int? Season { get; set; }

    [Column("day")]
    public int? Day { get; set; }

    [Column("territory_name", TypeName = "citext")]
    public string? TerritoryName { get; set; }

    [Column("team_name", TypeName = "citext")]
    public string? TeamName { get; set; }

    [Column("color")]
    public string? Color { get; set; }

    [Column("secondary_color")]
    public string? SecondaryColor { get; set; }

    [Column("tname", TypeName = "citext")]
    public string? Tname { get; set; }

    [Column("prev_owner", TypeName = "citext")]
    public string? PrevOwner { get; set; }

    [Column("mvp", TypeName = "citext")]
    public string? Mvp { get; set; }
}
