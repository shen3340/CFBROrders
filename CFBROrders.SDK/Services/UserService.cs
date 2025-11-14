using CFBROrders.SDK.Data_Models;
using CFBROrders.SDK.Interfaces;
using CFBROrders.SDK.Interfaces.Services;
using CFBROrders.SDK.Models;
using CFBROrders.SDK.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFBROrders.SDK.Services
{
    public class UserService(IUnitOfWork unitOfWork, IOperationResult result, ILogger<UserService> logger) : IUserService
    {
        public IUnitOfWork UnitOfWork { get; set; } = unitOfWork;
        public IOperationResult Result { get; set; } = result;

        private readonly ILogger _logger = logger;

        private NPoco.IDatabase Db => ((NPocoUnitOfWork)UnitOfWork).Db;

        public List<User> GetAllUsers()
        {
            Result.Reset();

            List<User> users;

            try
            {
                users = Db.Fetch<User>(
                    @"SELECT *
                      FROM users"
                ).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all users");

                Result.GetException(ex);

                throw;
            }
            _logger.LogInformation("Success getting all users");
            
            return users;
        }

        public User GetUserByPlatformAndUsername(string platform, string uname)
        {
            Result.Reset();

            User user;

            try
            {
                user = Db.SingleOrDefault<User>(
                    @"SELECT *
                      FROM users
                    WHERE platform = @0 
                    AND split_part(uname, '$', 1) = @1",
                    platform, uname
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting user with platform: {platform} and uname: {uname}");
                
                Result.GetException(ex);

                throw;
            }
            _logger.LogInformation($"Success getting user with platform: {platform} and uname: {uname}");
            
            return user;
        }
    }
}
