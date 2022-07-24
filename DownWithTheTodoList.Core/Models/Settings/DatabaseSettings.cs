using System.Text;

namespace DownWithTheTodoList.Core.Models.Settings;

public class DatabaseSettings
{
    public string Server {  get; init; }
    public Port? Port { get; set; } = null;
    public string Database { get; init; }
    public string User { get; init; }
    public string Password { get; init; }
    public Version Version { get; init; }
    
    public string ConnectionString { get; private init; } 

    public DatabaseSettings(string server, Port port, string database, string user, string password, Version version)
    {
        Server = server;
        Port = port;
        Database = database;
        User = user;
        Password = password;
        Version = version;

        ConnectionString = CreateConnetionString(this);
    }

    public DatabaseSettings(string server, string database, string user, string password, Version version)
    {
        Server = server;
        Database = database;
        User = user;
        Password = password;
        Version = version;

        ConnectionString = CreateConnetionString(this);
    }

    private string CreateConnetionString(DatabaseSettings databaseSettings)
    {
        StringBuilder builder = new();

        builder.Append("Server=").Append(Server).Append(';');

        if (Port is not null)
        {
            builder.Append("Port=").Append(Port.Value).Append(';');
        }

        builder.Append("Database=").Append(Database).Append(';');
        builder.Append("Uid=").Append(User).Append(';');
        builder.Append("Password=").Append(Password).Append(';');


        return builder.ToString();
    }



}
