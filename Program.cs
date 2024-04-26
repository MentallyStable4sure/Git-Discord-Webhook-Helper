using MentallyStable.GitHelper.Registrators;
using MentallyStable.GitHelper.Services;
using MentallyStable.GitHelper.Services.Discord;
using MentallyStable.GitHelper.Services.Discord.Bot;

var builder = WebApplication.CreateBuilder(args);

var configs = new ConfigsRegistrator();
await configs.Register(builder);

var registrators = new List<IRegistrator>()
{
    new ScopeRegistrator(),
    new SingleInstanceRegistrator(configs)
};

// Register our custom services for injection.

foreach (var registrator in registrators)
{
    await registrator.Register(builder);
}

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseAuthorization(); //For anyone who want claims/role-based auth
//app.UseHttpsRedirection(); //For anyone who is using HTTPS (im not)
app.MapControllers();

app.Run();
