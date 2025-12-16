using CFBROrders.SDK.Models;

namespace CFBROrders.SDK.Interfaces.Services
{
    public interface IUserService
    {
        public List<User> GetAllUsers();

        public User GetUserByPlatformAndUsername(string platform, string uname);

        public int GetOverallByUserId(int userId);

        public User GetUserById(int userId);
    }
}
