using CFBROrders.SDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFBROrders.SDK.Interfaces.Services
{
    public interface ITurnInfoService
    {
        public List<int> GetAllSeasons();

        public List<int> GetAllTurnsBySeason(int season);

        public int GetMostRecentCompletedTurnId();

        public int GetSeasonByTurnId(int turnId);

        public int GetTurnByTurnId(int turnId);
    }
}
