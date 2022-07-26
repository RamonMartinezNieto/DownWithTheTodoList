using DownWithTheTodoList.Ms.Users.Logger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfigurationSection settingsSections = builder.Configuration.GetSection("DataBaseConfig");
DatabaseSettings settings = settingsSections.CreateDatabaseSettings();

builder.Services
    .AddSingleton(settings)
    .AddMySqlDbContext(settings)
    .AddTransient<UsersContext>()
    .AddTransient<IUserRepository, UserRepository>()
    .AddTransient<IUserService, UserService>()
    .AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run();
