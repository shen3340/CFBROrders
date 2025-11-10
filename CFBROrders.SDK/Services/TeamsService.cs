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
    public class TeamsService : ITeamsService
    {
        public IUnitOfWork UnitOfWork { get; set; }

        private readonly ApplicationDBContext _context;

        private ILogger _logger;

        public TeamsService(IUnitOfWork unitOfWork, ApplicationDBContext context, ILogger<TeamsService> logger)
        {
            UnitOfWork = unitOfWork;
            _context = context;
            _logger = logger;
        }

        public double GetTeamStarPowerForTurn(string tname, int season, int day)
        {
            try
            {
                var starPower = ((NPocoUnitOfWork)UnitOfWork).db.SingleOrDefault<double>(
                    @"SELECT starpower
                      FROM statistics WHERE tname = @0 AND season = @1 AND day = @2", tname, season, day);

                return starPower;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching starpower for {tname}: Season {season}, Day {day}");
                return 0.0;
            }
            
        }
    }
}
