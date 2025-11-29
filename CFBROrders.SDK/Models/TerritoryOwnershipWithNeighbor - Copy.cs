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
        private static readonly JsonSerializerOptions _jsonOptions =
            new()
            { PropertyNameCaseInsensitive = true };

        public List<Neighbor> NeighborList { get; set; } = [];

        public int Tier { get; set; } = 1;

        public int StarPowerAllocation { get; set; }
    }
}
