using CFBROrders.SDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFBROrders.SDK.Interfaces.Services
{
    public interface ITerritoriesService
    {
        public List<TerritoryOwnershipWithNeighbor> GetTerritoryOwnershipWithNeighbors(int season, int day, string team);

        public List<(string Name, string Owner)> GetAttackableTerritories(string team, int season, int day);
    }
}
