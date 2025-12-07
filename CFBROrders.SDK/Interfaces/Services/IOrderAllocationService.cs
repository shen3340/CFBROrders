using CFBROrders.SDK.Data_Models;
using CFBROrders.SDK.Models;

namespace CFBROrders.SDK.Interfaces.Services
{
    public interface IOrderAllocationService
    {
        public List<OrderAllocation> GetAllOrderAllocations(int teamId, int seasonId, int turnId);

        public List<TierSummary> GetTierSummaries(IEnumerable<OrderAllocation> orderAllocations, IEnumerable<UserOrder> userOrders);

        public IOperationResult InsertOrderAllocation(OrderAllocation orderAllocation);

        public void InsertOrderAllocationWithoutTransaction(OrderAllocation orderAllocation);

        public IOperationResult InsertOrderAllocations(List<OrderAllocation> orderAllocations);

        public void RecalculateAllocationForTerritory(int seasonId, int turnId, int territoryId);
    }
}
