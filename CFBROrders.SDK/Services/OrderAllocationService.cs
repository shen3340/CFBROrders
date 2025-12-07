using CFBROrders.SDK.Data_Models;
using CFBROrders.SDK.Interfaces;
using CFBROrders.SDK.Interfaces.Services;
using CFBROrders.SDK.Models;
using CFBROrders.SDK.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CFBROrders.SDK.Services
{
    public class OrderAllocationService(IUnitOfWork unitOfWork, IOperationResult result,
        ILogger<OrderAllocationService> logger, ITerritoryService territoryService) : IOrderAllocationService
    {
        public IUnitOfWork UnitOfWork { get; set; } = unitOfWork;
        public IOperationResult Result { get; set; } = result;

        private readonly ILogger _logger = logger;
        public ITerritoryService TerritoryService { get; set; } = territoryService;

        private NPoco.IDatabase Db => ((NPocoUnitOfWork)UnitOfWork).Db;

        public List<OrderAllocation> GetAllOrderAllocations(int teamId, int seasonId, int turnId)
        {
            Result.Reset();

            List<OrderAllocation> orderAllocations;

            try
            {
                orderAllocations = Db.Fetch<OrderAllocation>(
                    @"
                    SELECT 
                        oa.*,
                        t.name AS TerritoryName
                    FROM order_allocations oa
                    JOIN territories t ON t.id = oa.territory_id
                    WHERE oa.team_id = @0
                      AND oa.season_id = @1
                      AND oa.turn_id = @2
                    ",
                    teamId,
                    seasonId,
                    turnId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching order allocations for TeamId {teamId} on Season {seasonId}, Turn {turnId}.");

                Result.GetException(ex);

                throw;
            }
            _logger.LogInformation($"Fetched order allocations for TeamId {teamId} on Season {seasonId}, Turn {turnId}.");

            return orderAllocations;
        }

        public List<TierSummary> GetTierSummaries(IEnumerable<OrderAllocation> orderAllocations, IEnumerable<UserOrder> userOrders)
        {
            return orderAllocations
           .GroupBy(allocation => allocation.Tier)
           .Select(group => new TierSummary
           {
               Tier = group.Key ?? 0,

               Players = userOrders
                   .Where(userOrder => group.Any(allocation => allocation.TerritoryId == userOrder.TerritoryId))
                   .Select(userOrder => userOrder.Username)
                   .Distinct()
                   .Count(),

               Quota = group.Sum(allocation => allocation.StarpowerAllocated ?? 0),
               AssignedStars = group.Sum(allocation => allocation.StarpowerUsed ?? 0),

               QuotaPercent = group.Sum(allocation => allocation.StarpowerAllocated ?? 0) > 0
                   ? (double)group.Sum(allocation => allocation.StarpowerUsed ?? 0) /
                     group.Sum(allocation => allocation.StarpowerAllocated ?? 0) * 100
                   : 0,

               TotalTerritories = group.Count(),
               CompletedTerritories = group.Count(allocation => allocation.IsTerritoryFull),

               CompletedPercent = group.Any()
                   ? (double)group.Count(allocation => allocation.IsTerritoryFull) /
                     group.Count() * 100
                   : 0
           })
           .OrderBy(t => t.Tier)
           .ToList();
        }

        public IOperationResult InsertOrderAllocation(OrderAllocation orderAllocation)
        {
            Result.Reset();

            try
            {
                UnitOfWork.BeginTransaction();

                Db.Insert(orderAllocation);

                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();

                _logger.LogError(ex, $"Error inserting single order allocation for {orderAllocation}");

                Result.GetException(ex);

                throw;
            }
            _logger.LogInformation($"Inserted Order Allocation: TerritoryId={orderAllocation.TerritoryId}, TeamId={orderAllocation.TeamId}");

            return Result;
        }

        public void InsertOrderAllocationWithoutTransaction(OrderAllocation orderAllocation)
        {
            Db.Insert(orderAllocation);
        }

        public IOperationResult InsertOrderAllocations(List<OrderAllocation> orderAllocations)
        {
            Result.Reset();

            try
            {
                UnitOfWork.BeginTransaction();

                foreach (var allocation in orderAllocations)
                {
                    InsertOrderAllocationWithoutTransaction(allocation);
                }

                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();

                _logger.LogError(ex, "Error batch inserting order allocations. Count={Count}", orderAllocations.Count);

                Result.GetException(ex);
                throw;
            }
            _logger.LogInformation("Batch insert successful: {Count} order allocations", orderAllocations.Count);

            return Result;
        }

        public void RecalculateAllocationForTerritory(int seasonId, int turnId, int territoryId)
        {
            long used = Db.SingleOrDefault<long>(@"
        SELECT COALESCE(SUM(starpower), 0)
        FROM user_orders
        WHERE season_id = @0
          AND turn_id = @1
          AND territory_id = @2
    ", seasonId, turnId, territoryId);

            Db.Execute(@"
        UPDATE order_allocations
        SET 
            starpower_used = @3,
            starpower_remaining = starpower_allocated - @3,
            is_territory_full = (starpower_allocated - @3) <= 0
        WHERE season_id = @0
          AND turn_id = @1
          AND territory_id = @2
    ", seasonId, turnId, territoryId, used);
        }

    }
}
