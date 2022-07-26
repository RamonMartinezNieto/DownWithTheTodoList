namespace DownWithTheTodoList.Core.Unit.Tests.Entities.Model.Settings;

public class DatabaseSettingsTests
{
    private readonly static string server = "server";
    private readonly static Port port = new(333);
    private readonly static string database = "database";
    private readonly static string user = "user";
    private readonly static string password = "password";
    private readonly static Version version = new("1.1.1");

    private readonly string connStringWithoutPort =
        $"Server={server};Database={database};Uid={user};Password={password};";

    private readonly string connStringWithPort =
        $"Server={server};Port={port};Database={database};Uid={user};Password={password};";


    [Fact]
    public void Should_Be_Valid_ConnectionString_Without_Port() 
    {
        DatabaseSettings settings = new(server, database,user,password,version);

        settings.ConnectionString.Should().Be(connStringWithoutPort);
    }

    [Fact]
    public void Should_Be_Valid_ConnectionString_With_Port() 
    {
        DatabaseSettings settings = new(server, port, database,user,password,version);

        settings.ConnectionString.Should().Be(connStringWithPort);
    }
}
