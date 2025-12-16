using CFBROrders.SDK.Data_Models;
using CFBROrders.SDK.Interfaces;
using CFBROrders.SDK.Interfaces.Services;
using CFBROrders.SDK.Repositories;
using Microsoft.Extensions.Logging;

namespace CFBROrders.SDK.Services
{
    public class TeamService(IUnitOfWork unitOfWork, IOperationResult result, ILogger<TeamService> logger) : ITeamService
    {
        public IUnitOfWork UnitOfWork { get; set; } = unitOfWork;
        public IOperationResult Result { get; set; } = result;

        private readonly ILogger _logger = logger;

        private NPoco.IDatabase Db => ((NPocoUnitOfWork)UnitOfWork).Db;

        public string GetTeamNameByTeamId(int id)
        {
            Result.Reset();

            string teamName;

            try
            {
                teamName = Db.SingleOrDefault<string>(
                    @"SELECT tname
                      FROM teams WHERE id = @0", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching teamName for {id}");

                Result.GetException(ex);

                throw;
            }
            _logger.LogInformation($"Fetched teamName for {id}");

            return teamName;
        }

        public int GetTeamIdByTeamName(string name)
        {
            Result.Reset();

            int teamId;

            try
            {
                teamId = Db.SingleOrDefault<int>(
                    @"SELECT id
                      FROM teams WHERE tname = @0", name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching id for {name}");

                Result.GetException(ex);

                throw;
            }
            _logger.LogInformation($"Fetched id for {name}");

            return teamId;
        }

        public string GetTeamColorByTeamId(int id)
        {
            Result.Reset();

            string color;

            try
            {
                color = Db.SingleOrDefault<string>(
                    @"SELECT color_1
                      FROM teams WHERE id = @0", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching color for team id:{id}");

                Result.GetException(ex);

                throw;
            }
            _logger.LogInformation($"Fetched color for team id: {id}");

            return color;

        }

        public int GetTeamStarPowerForTurn(string tname, int season, int day)
        {
            Result.Reset();

            int starpower;

            try
            {
                starpower = Db.SingleOrDefault<int>(
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
