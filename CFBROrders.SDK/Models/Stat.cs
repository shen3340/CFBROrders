using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
[Table("stats")]
public partial class Stat
{
    [Column("team")]
    public int? Team { get; set; }

    [Column("rank")]
    public int? Rank { get; set; }

    [Column("territorycount")]
    public int? Territorycount { get; set; }

    [Column("playercount")]
    public int? Playercount { get; set; }

    [Column("merccount")]
    public int? Merccount { get; set; }

    [Column("starpower")]
    public double? Starpower { get; set; }

    [Column("efficiency")]
    public double? Efficiency { get; set; }

    [Column("effectivepower")]
    public double? Effectivepower { get; set; }

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

    [Column("turn_id")]
    public int? TurnId { get; set; }
}
