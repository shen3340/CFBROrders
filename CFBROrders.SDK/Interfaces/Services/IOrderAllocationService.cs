using CFBROrders.SDK.Data_Models;
using CFBROrders.SDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFBROrders.SDK.Interfaces.Services
{
    public interface IOrderAllocationService
    {
        public List<OrderAllocation> GetAllOrderAllocations(int teamId, int seasonId, int turnId);

        public IOperationResult InsertOrderAllocation(OrderAllocation orderAllocation);
        
        public void InsertOrderAllocationWithoutTransaction(OrderAllocation orderAllocation);
        
        public IOperationResult InsertOrderAllocations(List<OrderAllocation> orderAllocations);

        public void RecalculateAllocationForTerritory(int seasonId, int turnId, int territoryId);
    }
}
