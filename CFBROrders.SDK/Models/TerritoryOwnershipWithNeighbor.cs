using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
public partial class TerritoryOwnershipWithNeighbor
{
    [Column("territory_id")]
    public int? TerritoryId { get; set; }

    [Column("day")]
    public int? Day { get; set; }

    [Column("season")]
    public int? Season { get; set; }

    [Column("name", TypeName = "citext")]
    public string? Name { get; set; }

    [Column("tname", TypeName = "citext")]
    public string? Tname { get; set; }

    [Column("region")]
    public int? Region { get; set; }

    [Column("region_name", TypeName = "citext")]
    public string? RegionName { get; set; }

    [Column("neighbors", TypeName = "json")]
    public string? Neighbors { get; set; }

    public int Priority { get; set; } = 1;

    [NotMapped]
    public List<Neighbor> NeighborList
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Neighbors))
                return new List<Neighbor>();

            try
            {
                return JsonSerializer.Deserialize<List<Neighbor>>(Neighbors) ?? new List<Neighbor>();
            }
            catch
            {
                return new List<Neighbor>();
            }
        }
    }
    public class Neighbor
    {
        public int id { get; set; }
        public string name { get; set; } = "";
        public string shortName { get; set; } = "";
        public string owner { get; set; } = "";
    }
}
