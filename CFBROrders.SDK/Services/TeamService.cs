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
    public class TeamService(IUnitOfWork unitOfWork, IOperationResult result, ILogger<TeamService> logger) : ITeamService
    {
        public IUnitOfWork UnitOfWork { get; set; } = unitOfWork;
        public IOperationResult Result { get; set; } = result;

        private readonly ILogger _logger = logger;

        private NPoco.IDatabase Db => ((NPocoUnitOfWork)UnitOfWork).Db;

        public double GetTeamStarPowerForTurn(string tname, int season, int day)
        {
            Result.Reset();

            double starpower;

            try
            {
                starpower = Db.SingleOrDefault<double>(
                    @"SELECT starpower
                      FROM statistics WHERE tname = @0 AND season = @1 AND day = @2", tname, season, day);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching starpower for {tname}: Season {season}, Day {day}");
                
                Result.GetException(ex);

                throw;
            }
            _logger.LogInformation($"Fetched starpower for {tname}: Season {season}, Day {day} - StarPower: {starpower}");

            return starpower;
        }
    }
}
