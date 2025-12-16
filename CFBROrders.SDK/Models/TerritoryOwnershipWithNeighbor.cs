using System.Text.Json;

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
