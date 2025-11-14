using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CFBROrders.SDK.Models
{
    public partial class TerritoryOwnershipWithNeighbor
    {
        public List<Neighbor> NeighborList
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Neighbors))
                    return [];

                try
                {
                    return JsonSerializer.Deserialize<List<Neighbor>>(Neighbors) ?? [];
                }
                catch
                {
                    return [];
                }
            }
        }

        public int Priority { get; set; } = 1;
    }

    public class Neighbor
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string ShortName { get; set; } = "";
        public string Owner { get; set; } = "";
    }
}
