using MentallyStable.GitlabHelper.Registrators;

var builder = WebApplication.CreateBuilder(args);
ScopeRegistrator scopeRegistrator = new ScopeRegistrator();
SingleInstanceRegistrator singleInstanceRegistrator = new SingleInstanceRegistrator();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register our custom services for injection.

await scopeRegistrator.Register(builder);
await singleInstanceRegistrator.Register(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseAuthorization();
//app.UseHttpsRedirection(); //For anyone who is using HTTPS (im not)
app.MapControllers();

app.Run();
