using CFBROrders.SDK.Data_Models;
using CFBROrders.SDK.Interfaces;
using CFBROrders.SDK.Interfaces.Services;
using CFBROrders.SDK.Models;
using CFBROrders.SDK.Repositories;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CFBROrders.SDK.Services
{
    public class TerritoryService(IUnitOfWork unitOfWork, IOperationResult result, ILogger<TerritoryService> logger) : ITerritoryService
    {
        public IUnitOfWork UnitOfWork { get; set; } = unitOfWork;
        public IOperationResult Result { get; set; } = result;

        private readonly ILogger _logger = logger;

        private NPoco.IDatabase Db => ((NPocoUnitOfWork)UnitOfWork).Db;

        private static readonly JsonSerializerOptions _jsonOptions =
            new() { PropertyNameCaseInsensitive = true };

        private static void ParseNeighbors(IEnumerable<TerritoryOwnershipWithNeighbor> territories)
        {
            foreach (var t in territories)
            {
                if (!string.IsNullOrWhiteSpace(t.Neighbors))
                {
                    try
                    {
                        t.NeighborList =
                            JsonSerializer.Deserialize<List<Neighbor>>(t.Neighbors, _jsonOptions)
                            ?? [];
                    }
                    catch
                    {
                        t.NeighborList = [];
                    }
                }
                else
                {
                    t.NeighborList = [];
                }
            }
        }

        public List<TerritoryOwnershipWithNeighbor> GetDefendableTerritories(int season, int day, string team)
        {
            Result.Reset();

            List<TerritoryOwnershipWithNeighbor> teams;

            try
            {
                teams = Db.Fetch<TerritoryOwnershipWithNeighbor>(
                    @"
                    SELECT *
                    FROM territory_ownership_with_neighbors t
                    WHERE t.season = @0
                      AND t.day = @1
                      AND t.tname = @2
                      AND EXISTS (
                          SELECT 1
                          FROM jsonb_array_elements(t.neighbors::jsonb) AS n(neighbor)
                          WHERE neighbor->>'owner' <> t.tname
                      )
                    ",
                    season, day, team
                ).ToList();

                ParseNeighbors(teams);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching defending territories for {team} on season {season} day {day}.");
                Result.GetException(ex);
                throw;
            }

            _logger.LogInformation($"Fetched defending territories for Season {season}, Day {day}, Team {team}.");
            return teams;
        }

        public List<TerritoryOwnershipWithNeighbor> GetAttackableTerritories(int season, int day, string team)
        {
            Result.Reset();

            List<TerritoryOwnershipWithNeighbor> territories;

            try
            {
                territories = Db.Fetch<TerritoryOwnershipWithNeighbor>(
                    @"
                    SELECT *
                    FROM territory_ownership_with_neighbors t
                    WHERE t.season = @0
                      AND t.day = @1
                      AND t.tname <> @2
                      AND EXISTS (
                            SELECT 1
                            FROM jsonb_array_elements(t.neighbors::jsonb) AS n(neighbor)
                            WHERE neighbor->>'owner' = @2
                      )
                      AND NOT EXISTS (
                            SELECT 1
                            FROM jsonb_array_elements(t.neighbors::jsonb) AS n(neighbor)
                            WHERE (neighbor->>'id')::int = 131
                      );
                    ",
                    season, day, team
                ).ToList();

                ParseNeighbors(territories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    $"Error fetching attackable territories for {team} (Season {season}, Day {day}).");
                Result.GetException(ex);
                throw;
            }

            _logger.LogInformation($"Fetched attackable territories for Season {season}, Day {day}, Team {team}.");
            return territories;
        }

        public string GetTerritoryNameByTerritoryId(int? id)
        {
            Result.Reset();

            string territoryName;

            try
            {
                territoryName = Db.SingleOrDefault<string>(
                    @"
                    SELECT name
                    FROM territories 
                    WHERE id = @0
                    ", id ?? (object)DBNull.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching territory name for ID {id}.");
               
                Result.GetException(ex);
                
                throw;
            }
            _logger.LogInformation($"Fetched territory name for ID {id}: {territoryName}.");
            
            return territoryName;
        }
    }
}
