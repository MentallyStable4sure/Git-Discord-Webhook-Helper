using Newtonsoft.Json;
using MentallyStable.GitlabHelper.Data;
using MentallyStable.GitlabHelper.Data.Database;
using MentallyStable.GitlabHelper.Data.Development;
using MentallyStable.GitlabHelper.Services.Development;
using MentallyStable.GitlabHelper.Services.Discord.Bot;

namespace MentallyStable.GitlabHelper.Registrators
{
    public sealed class SingleInstanceRegistrator : IRegistrator
    {
        public DatabaseConfig DatabaseConfig { get; private set; }

        private Debugger debugger = new Debugger();
        private ServerConfig serverConfig;
        private DiscordConfig discordConfig;

        public async Task Register(WebApplicationBuilder builder)
        {
            var dbConfig = await LoadConfig("dbconfig.json");
            var svConfig = await LoadConfig("serverconfig.json");
            var dsConfig = await LoadConfig("discordconfig.json");

            debugger.TryExecute(() => DatabaseConfig = ConvertConfig<DatabaseConfig>(dbConfig), new DebugOptions(this));
            debugger.TryExecute(() => serverConfig = ConvertConfig<ServerConfig>(svConfig), new DebugOptions(this));
            debugger.TryExecute(() => discordConfig = ConvertConfig<DiscordConfig>(dsConfig), new DebugOptions(this));

            var discordBot = new DiscordBotWrapper(discordConfig);

            builder.Services.AddSingleton<ServerConfig>(serverConfig);
            builder.Services.AddSingleton<DiscordBotWrapper>(discordBot);

            await discordBot.Connect().ConfigureAwait(false);
        }

        private async Task<string> LoadConfig(string config) => await DataGrabber.GrabFromConfigs(config);

        private T ConvertConfig<T>(string rawJson) => JsonConvert.DeserializeObject<T>(rawJson);
    }
}
