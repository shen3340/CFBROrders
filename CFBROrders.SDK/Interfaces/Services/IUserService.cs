using CFBROrders.SDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFBROrders.SDK.Interfaces.Services
{
    public interface IUserService
    {
        public List<User> GetAllUsers();

        public User GetUserByPlatformAndUsername(string platform, string uname);
    }
}
