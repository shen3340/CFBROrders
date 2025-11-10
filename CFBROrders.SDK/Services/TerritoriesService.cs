using CFBROrders.SDK.Data;
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
    public class TerritoriesService : ITerritoriesService
    {
        public IUnitOfWork UnitOfWork { get; set; }

        private readonly ApplicationDBContext _context;

        private ILogger _logger;

        public TerritoriesService(IUnitOfWork unitOfWork, ApplicationDBContext context, ILogger<TeamsService> logger)
        {
            UnitOfWork = unitOfWork;
            _context = context;
            _logger = logger;
        }

        public List<TerritoryOwnershipWithNeighbor> GetTerritoryOwnershipWithNeighbors(int season, int day, string team )
        {
            var Teams = new List<TerritoryOwnershipWithNeighbor>();
            try
            {
                Teams = ((NPocoUnitOfWork)UnitOfWork).db.Fetch<TerritoryOwnershipWithNeighbor>(
                    @"SELECT 
                        *
                      FROM territory_ownership_with_neighbors WHERE season = @0 AND day = @1
                        AND tName = @2", season, day, team
                ).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all Teams with neighbors.");
            }
            return Teams;
        }

        public List<(string Name, string Owner)> GetAttackableTerritories(string team, int season, int day)
        {
            var territories = GetTerritoryOwnershipWithNeighbors(season, day, team);

            return territories.SelectMany(t => t.NeighborList)
                              .Where(n => n.owner != team)
                              .GroupBy(n => n.name)
                              .Select(g => (g.Key, g.First().owner))
                              .OrderBy(t => t.Key)
                              .ToList();
        }
    }
}
