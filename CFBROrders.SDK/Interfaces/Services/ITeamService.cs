namespace CFBROrders.SDK.Interfaces.Services
{
    public interface ITeamService
    {
        public string GetTeamNameByTeamId(int id);

        public int GetTeamIdByTeamName(string name);

        public string GetTeamColorByTeamId(int id);

        public int GetTeamStarPowerForTurn(string tname, int season, int day);
    }
}
