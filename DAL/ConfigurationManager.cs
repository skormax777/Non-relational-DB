using MongoDB.Driver;

namespace DAL
{
    public class ConfigurationManager
    {
        private ConfigurationManager()
        {
        }

        public static IMongoDatabase GetDefaultDatabase()
        {
            var connectionString = GetDefaultConnectionString();
            var client = new MongoClient(connectionString);
            return client.GetDatabase(GetDefaultDatabaseName());
        }

        private static string GetDefaultConnectionString()
        {
            return "mongodb://localhost:27017";
        }

        private static string GetDefaultDatabaseName()
        {
            return "SN";
        }
    }
}
