using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
public partial class TerritoryNeighborHistory
{
    [Column("turn_id")]
    public int? TurnId { get; set; }

    [Column("id")]
    public int? Id { get; set; }

    [Column("neighbors", TypeName = "json")]
    public string? Neighbors { get; set; }
}
