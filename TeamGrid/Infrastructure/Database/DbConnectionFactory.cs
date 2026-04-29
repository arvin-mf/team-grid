using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

public interface IDbConnectionFactory
{
    IDbConnection Create();
}

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    static DbConnectionFactory()
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public DbConnectionFactory(IConfiguration config)
    {
        var host = config["DB_HOST"];
        var port = config["DB_PORT"];
        var name = config["DB_NAME"];
        var user = config["DB_USER"];
        var pass = config["DB_PASS"];

        _connectionString = $"Host={host};Port={port};Database={name};Username={user};Password={pass}";
    } 

    public IDbConnection Create()
    {
        return new NpgsqlConnection(_connectionString);
    }
}