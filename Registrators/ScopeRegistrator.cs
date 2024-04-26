using MentallyStable.GitHelper.Services.Development;

namespace MentallyStable.GitHelper.Registrators
{
    public class ScopeRegistrator : IRegistrator
    {
        public Task Register(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IDebugger, Debugger>(); //we will change to the UserService on release

            return Task.CompletedTask;
        }
    }
}
