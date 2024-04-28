using MentallyStable.GitHelper.Data.Git.Gitlab;
using MentallyStable.GitHelper.Services.Parsers;
using MentallyStable.GitHelper.Services.Discord;
using MentallyStable.GitHelper.Services.Development;
using MentallyStable.GitHelper.Services.Parsers.Implementation;

namespace MentallyStable.GitHelper.Registrators
{
    public class ScopeRegistrator : IRegistrator
    {
        public Task Register(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IDebugger, Debugger>();
            builder.Services.AddScoped<IResponseParser<GitlabResponse>, GitlabResponseParser>(); //can be changed to any parser

            return Task.CompletedTask;
        }
    }
}
