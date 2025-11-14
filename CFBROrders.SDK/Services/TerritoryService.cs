using CFBROrders.SDK.Data_Models;
using CFBROrders.SDK.Interfaces;
using CFBROrders.SDK.Interfaces.Services;
using CFBROrders.SDK.Models;
using CFBROrders.SDK.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFBROrders.SDK.Services
{
    public class TerritoryService(IUnitOfWork unitOfWork, IOperationResult result, ILogger<TeamService> logger) : ITerritoryService
    {
        public IUnitOfWork UnitOfWork { get; set; } = unitOfWork;
        public IOperationResult Result { get; set; } = result;

        private readonly ILogger _logger = logger;

        private NPoco.IDatabase Db => ((NPocoUnitOfWork)UnitOfWork).Db;

        public List<TerritoryOwnershipWithNeighbor> GetTerritoryOwnershipWithNeighbors(int season, int day, string team )
        {
            Result.Reset();

            List<TerritoryOwnershipWithNeighbor> teams;

            try
            {
                teams = Db.Fetch<TerritoryOwnershipWithNeighbor>(
                    @"SELECT 
                        *
                      FROM territory_ownership_with_neighbors WHERE season = @0 AND day = @1
                        AND tName = @2", season, day, team
                ).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all Teams with neighbors.");
                
                Result.GetException(ex);

                throw;
            }
            _logger.LogInformation($"Fetched territory ownership with neighbors for Season {season}, Day {day}, and Team {team}.");
            
            return teams;
        }

        public List<(string Name, string Owner)> GetAttackableTerritories(string team, int season, int day)
        {
            var territories = GetTerritoryOwnershipWithNeighbors(season, day, team);

            return territories.SelectMany(t => t.NeighborList)
                              .Where(n => n.Owner != team)
                              .GroupBy(n => n.Name)
                              .Select(g => (g.Key, g.First().Owner))
                              .OrderBy(t => t.Key)
                              .ToList();
        }
    }
}
