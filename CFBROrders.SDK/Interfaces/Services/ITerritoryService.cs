using CFBROrders.SDK.Models;

namespace CFBROrders.SDK.Interfaces.Services
{
    public interface ITerritoryService
    {
        public List<TerritoryOwnershipWithNeighbor> GetDefendableTerritories(int season, int day, string team);

        public List<TerritoryOwnershipWithNeighbor> GetAttackableTerritories(int season, int day, string team);

        public string GetTerritoryNameByTerritoryId(int? id);
    }
}
