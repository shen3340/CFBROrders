using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
[Table("territory_adjacency")]
public partial class TerritoryAdjacency
{
    [Column("id")]
    public int? Id { get; set; }

    [Column("territory_id")]
    public int? TerritoryId { get; set; }

    [Column("adjacent_id")]
    public int? AdjacentId { get; set; }

    [Column("note")]
    public string? Note { get; set; }

    [Column("min_turn")]
    public int? MinTurn { get; set; }

    [Column("max_turn")]
    public int? MaxTurn { get; set; }
}
