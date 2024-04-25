using Newtonsoft.Json;
using MentallyStable.GitlabHelper.Data;
using MentallyStable.GitlabHelper.Data.Database;
using MentallyStable.GitlabHelper.Data.Development;
using MentallyStable.GitlabHelper.Services.Development;

namespace MentallyStable.GitlabHelper.Registrators
{
    public sealed class SingleInstanceRegistrator : IRegistrator
    {
        public DatabaseConfig DatabaseConfig { get; private set; }

        private Debugger debugger = new Debugger();
        private ServerConfig serverConfig;

        public async Task Register(WebApplicationBuilder builder)
        {
            var dbConfig = await LoadDatabaseConfig();
            var svConfig = await LoadServerConfig();
            debugger.TryExecute(() => DatabaseConfig = ConvertConfig<DatabaseConfig>(dbConfig), new DebugOptions(this));
            debugger.TryExecute(() => serverConfig = ConvertConfig<ServerConfig>(svConfig), new DebugOptions(this));

            builder.Services.AddSingleton<ServerConfig>(serverConfig);
        }

        private async Task<string> LoadDatabaseConfig() => await DataGrabber.GrabFromConfigs("dbconfig.json");
        private async Task<string> LoadServerConfig() => await DataGrabber.GrabFromConfigs("serverconfig.json");

        private T ConvertConfig<T>(string rawJson) => JsonConvert.DeserializeObject<T>(rawJson);
    }
}
