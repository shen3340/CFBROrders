using CFBROrders.SDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFBROrders.SDK.Interfaces.Services
{
    public interface ITeamsService
    {
        public double GetTeamStarPowerForTurn(string tname, int season, int day);
    }
}
