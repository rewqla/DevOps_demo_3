using System;

namespace OilShop.Helpers
{
    public static class ConnenctionStringHelper
    {
        public static string GenerateConnectionString()
        {
            var dbHost = Environment.GetEnvironmentVariable("DbHost");
            var dbUser = Environment.GetEnvironmentVariable("DbUsername");
            var dbPass = Environment.GetEnvironmentVariable("DbPassword");
            var dbName = Environment.GetEnvironmentVariable("DbName");

            if (dbHost == null && dbUser == null && dbPass == null && dbName == null)
                return "";

            dbHost = dbHost.Split(':')[0];

            string connectionString = $"Server={dbHost};Port=3306;Database={dbName};User Id={dbUser};Password={dbPass};Connect Timeout=30;SslMode=None";

            return connectionString;

        }
    }
}
