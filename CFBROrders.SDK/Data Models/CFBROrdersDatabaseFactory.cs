using CFBROrders.SDK.Data_Models;
using Npgsql;
using NPoco;
using NPoco.FluentMappings;

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
            var fluentConfig = FluentMappingConfiguration.Configure(new NPocoModelMappings());



            DbFactory = DatabaseFactory.Config(x =>
            {
                x.UsingDatabase(() =>
                    new Database(connectionString, DatabaseType.PostgreSQL, NpgsqlFactory.Instance)
                );
                x.WithFluentConfig(fluentConfig);
            });
        }
    }
}