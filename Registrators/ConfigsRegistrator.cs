using Newtonsoft.Json;
using MentallyStable.GitlabHelper.Data;
using MentallyStable.GitlabHelper.Data.Discord;
using MentallyStable.GitlabHelper.Data.Database;
using MentallyStable.GitlabHelper.Data.Development;
using MentallyStable.GitlabHelper.Services.Development;

namespace MentallyStable.GitlabHelper.Registrators
{
    public sealed class ConfigsRegistrator : IRegistrator
    {
        public DatabaseConfig DatabaseConfig { get; private set; }
        public ServerConfig ServerConfig { get; private set; }
        public DiscordConfig DiscordConfig { get; private set; }

        public Dictionary<ulong, BroadcastData> BroadcastData { get; private set; } = new Dictionary<ulong, BroadcastData>();

        private readonly IDebugger _debugger = new Debugger();

        public async Task Register(WebApplicationBuilder builder)
        {
            await LoadConfigs();

            builder.Services.AddSingleton<ServerConfig>(ServerConfig);
            builder.Services.AddSingleton<DiscordConfig>(DiscordConfig);
            builder.Services.AddSingleton<Dictionary<ulong, BroadcastData>>(BroadcastData);
        }

        private async Task LoadConfigs()
        {
            var dbConfig = await LoadConfig("dbconfig.json");
            var svConfig = await LoadConfig("serverconfig.json");
            var dsConfig = await LoadConfig("discordconfig.json");
            var broadcastersConfig = await LoadConfig("discordbroadcasters.json");

            _debugger.TryExecute(() => DatabaseConfig = ConvertConfig<DatabaseConfig>(dbConfig), new DebugOptions(this, typeof(DatabaseConfig).Name));
            _debugger.TryExecute(() => ServerConfig = ConvertConfig<ServerConfig>(svConfig), new DebugOptions(this, typeof(ServerConfig).Name));
            _debugger.TryExecute(() => DiscordConfig = ConvertConfig<DiscordConfig>(dsConfig), new DebugOptions(this, typeof(DiscordConfig).Name));
            _debugger.TryExecute(() => BroadcastData = ConvertConfig<Dictionary<ulong, BroadcastData>>(broadcastersConfig), new DebugOptions(this, broadcastersConfig.GetType().Name));
        }

        private async Task<string> LoadConfig(string config) => await DataGrabber.GrabFromConfigs(config);

        private T ConvertConfig<T>(string rawJson) => JsonConvert.DeserializeObject<T>(rawJson);
    }
}
