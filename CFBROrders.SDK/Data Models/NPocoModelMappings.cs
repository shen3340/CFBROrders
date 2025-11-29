using CFBROrders.SDK.Models;
using NPoco.FluentMappings;

namespace CFBROrders.SDK.Data_Models
{
    public class NPocoModelMappings : Mappings
    {
        public NPocoModelMappings()
        {
            For<TerritoryOwnershipWithNeighbor>()
                .Columns(x =>
                {
                    x.Column(y => y.NeighborList).Result();
                    x.Column(y => y.Tier).Result();
                    x.Column(y => y.StarPowerAllocation).Result();
                });

            For<OrderAllocation>()
                .TableName("order_allocations")
                .PrimaryKey("id")
                .Columns(x =>
                {
                    x.Column(y => y.TerritoryName).Result();
                });
        }
    }
}