using NPoco;

namespace CFBROrders.SDK.Models
{
    public partial class OrderAllocation
    {
        [ResultColumn]
        public string TerritoryName { get; set; }
    }
}
