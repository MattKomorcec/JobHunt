using System;
using Contracts;

namespace Web.Configuration;

public static class Database
{
    /// <summary>
    /// Tries to get the connection string, either from an env variable set in Azure,
    /// or from appsettings.json file when in development.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The connection string, or an empty string.</returns>
	public static string GetConnectionString(ConfigurationManager configuration)
	{
        var connectionString = Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_AZURE_POSTGRESQL_CONNECTIONSTRING");

        if (string.IsNullOrEmpty(connectionString))
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        if (string.IsNullOrEmpty(connectionString))
        {
            connectionString = string.Empty;
        }

        return connectionString;
    }
}

