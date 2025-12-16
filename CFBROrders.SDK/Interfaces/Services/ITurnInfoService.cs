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
