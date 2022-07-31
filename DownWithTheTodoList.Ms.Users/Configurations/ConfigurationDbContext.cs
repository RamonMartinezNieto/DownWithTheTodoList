namespace DownWithTheTodoList.Ms.Users.Configurations;

public static class ConfigurationDbContext
{
    public static DatabaseSettings CreateDatabaseSettings(this IConfigurationSection section) 
    {
        return new DatabaseSettings(
            section["Server"],
            (Port)int.Parse(section["Port"]),
            section["Database"],
            section["User"],
            section["Password"],
            new Version(section["Version"]));
    }

    public static IServiceCollection AddMySqlDbContext(this IServiceCollection services, DatabaseSettings settings) 
    {
        services.AddDbContext<UsersContext>(
              dbContextOptions => dbContextOptions
                  .UseMySql(settings.ConnectionString, new MySqlServerVersion(settings.Version))
                  //.LogTo(Console.WriteLine, LogLevel.Information)
                  //.EnableSensitiveDataLogging()
                  .EnableDetailedErrors()
          );
        
        return services;
    }
}
