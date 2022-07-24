var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfigurationSection settingsSections = builder.Configuration.GetSection("DataBaseConfig");
DatabaseSettings settings = settingsSections.CreateDatabaseSettings();

builder.Services
    .AddSingleton(settings)
    .AddMySqlDbContext(settings)
    .AddScoped<UsersContext>()
    .AddScoped<UsersRepository>();

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
