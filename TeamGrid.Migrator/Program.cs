using System.Reflection;
using DbUp;
using DotNetEnv;

class Program
{
    static int Main(string[] args)
    {
        var root = Directory.GetCurrentDirectory();
        var envPath = Path.Combine(root, ".env");
        Env.Load(envPath);

        var host = Env.GetString("DB_HOST");
        var port = Env.GetString("DB_PORT");
        var user = Env.GetString("DB_USER");
        var pass = Env.GetString("DB_PASS");
        var name = Env.GetString("DB_NAME");

        var connectionString = $"Host={host};Port={port};Database={name};Username={user};Password={pass}";

        var filesPath = Path.Combine(root, "TeamGrid", "Infrastructure", "Database", "Migrations");

        EnsureDatabase.For.PostgresqlDatabase(connectionString);

        var upgrader = DeployChanges.To
            .PostgresqlDatabase(connectionString)
            .WithScriptsFromFileSystem(filesPath)
            .Build();

        Console.WriteLine($"Performing migration to database: {name} di {host}...");
        
        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Migration failed");
            Console.WriteLine(result.Error);
            Console.ResetColor();
            return -1;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Migration succeeded");
        Console.ResetColor();
        return 0;
    }
}