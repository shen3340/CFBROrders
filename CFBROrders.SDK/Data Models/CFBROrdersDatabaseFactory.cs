using Npgsql;
using NPoco;

namespace CFBROrders.SDK.DataModel
{
    public class CFBROrdersDatabaseFactory
    {
        public static DatabaseFactory DbFactory { get; set; }

        public static string ConnectionString
        {
            get
            {
                return DbFactory.GetDatabase().ConnectionString;
            }
        }

        public static void Setup(string connectionString)
        {
            DbFactory = DatabaseFactory.Config(x =>
            {
                x.UsingDatabase(() =>
                    new Database(connectionString, DatabaseType.PostgreSQL, NpgsqlFactory.Instance)
                );
            });
        }
    }
}