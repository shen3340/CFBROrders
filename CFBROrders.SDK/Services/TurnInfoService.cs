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
    public class TurnInfoService(IUnitOfWork unitOfWork, IOperationResult result, ILogger<TeamService> logger) : ITurnInfoService
    {
        public IUnitOfWork UnitOfWork { get; set; } = unitOfWork;
        public IOperationResult Result { get; set; } = result;

        private readonly ILogger _logger = logger;

        private NPoco.IDatabase Db => ((NPocoUnitOfWork)UnitOfWork).Db;

        public List<int> GetAllSeasons()
        {
            Result.Reset();

            List<int> turns;

            try
            {
                turns = Db.Fetch<int>(
                    @"SELECT DISTINCT
                       season FROM turninfo ORDER BY season DESC");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching all seasons");

                Result.GetException(ex);

                throw;
            }
            _logger.LogInformation($"Fetched all seasons");

            return turns;
        }

        public List<int> GetAllTurnsBySeason(int season)
        {
            Result.Reset();
            List<int> turns;
            try
            {
                turns = Db.Fetch<int>(
                    @"SELECT day
                      FROM turninfo 
                      WHERE season = @0
                      ORDER BY day DESC", season);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching all turns for season: {season}");
                
                Result.GetException(ex);
                
                throw;
            }
            _logger.LogInformation($"Fetched all turns for season: {season}");
            
            return turns;
        }

        public int GetMostRecentCompletedTurnId()
        {
            Result.Reset();

            int turnId;

            try
            {
                turnId = Db.SingleOrDefault <int>(
                    @"SELECT id
                      FROM turninfo 
                      WHERE complete = 'true' AND active = 'false' 
                      ORDER BY id DESC LIMIT 1");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching latest completed turn");

                Result.GetException(ex);

                throw;
            }
            _logger.LogInformation($"Fetched latest completed turn");

            return turnId;
        }

        public int GetSeasonByTurnId(int turnId)
        {
            Result.Reset();
            int season;
            try
            {
                season = Db.SingleOrDefault<int>(
                    @"SELECT season
                      FROM turninfo 
                      WHERE id = @0", turnId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching season for turnId: {turnId}");
                
                Result.GetException(ex);
                
                throw;
            }
            _logger.LogInformation($"Fetched season for turnId: {turnId}");

            return season;
        }

        public int GetTurnByTurnId(int turnId)
        {
            Result.Reset();
            int turn;
            try
            {
                turn = Db.SingleOrDefault<int>(
                    @"SELECT day
                      FROM turninfo 
                      WHERE id = @0", turnId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching day for turnId: {turnId}");
                
                Result.GetException(ex);
                
                throw;
            }
            _logger.LogInformation($"Fetched day for turnId: {turnId}");

            return turn;
        }
    }
}
