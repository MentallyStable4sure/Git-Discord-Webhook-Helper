using Newtonsoft.Json;
using MentallyStable.GitHelper.Data;
using MentallyStable.GitHelper.Data.Discord;
using MentallyStable.GitHelper.Data.Database;
using MentallyStable.GitHelper.Data.Development;
using MentallyStable.GitHelper.Services.Development;

namespace MentallyStable.GitHelper.Registrators
{
    public sealed class ConfigsRegistrator : IRegistrator
    {
        public DatabaseConfig DatabaseConfig { get; private set; }
        public ServerConfig ServerConfig { get; private set; }
        public DiscordConfig DiscordConfig { get; private set; }
        public List<GitToDiscordLinkData> LinkData { get; private set; }

        public Dictionary<ulong, BroadcastData> BroadcastData { get; private set; } = new Dictionary<ulong, BroadcastData>();

        private readonly IDebugger _debugger = new Debugger();

        public async Task Register(WebApplicationBuilder builder)
        {
            await LoadConfigs();

            builder.Services.AddSingleton<ServerConfig>(ServerConfig);
            builder.Services.AddSingleton<DiscordConfig>(DiscordConfig);
            builder.Services.AddSingleton<Dictionary<ulong, BroadcastData>>(BroadcastData);
            builder.Services.AddSingleton<List<GitToDiscordLinkData>>(LinkData);
        }

        private async Task LoadConfigs()
        {
            var dbConfig = await LoadConfig("dbconfig.json");
            var svConfig = await LoadConfig("serverconfig.json");
            var dsConfig = await LoadConfig(Endpoints.DISCORD_CONFIG);
            var broadcastersConfig = await LoadConfig(Endpoints.DISCORD_BROADCASTERS_CONFIG);
            var linkDataConfig = await LoadConfig(Endpoints.ESTABLISHED_CONNECTIONS_CONFIG);

            _debugger.TryExecute(() => DatabaseConfig = ConvertConfig<DatabaseConfig>(dbConfig), new DebugOptions(this, typeof(DatabaseConfig).Name));
            _debugger.TryExecute(() => ServerConfig = ConvertConfig<ServerConfig>(svConfig), new DebugOptions(this, typeof(ServerConfig).Name));
            _debugger.TryExecute(() => DiscordConfig = ConvertConfig<DiscordConfig>(dsConfig), new DebugOptions(this, typeof(DiscordConfig).Name));
            _debugger.TryExecute(() => BroadcastData = ConvertConfig<Dictionary<ulong, BroadcastData>>(broadcastersConfig), new DebugOptions(this, broadcastersConfig.GetType().Name));
            _debugger.TryExecute(() => LinkData = ConvertConfig<List<GitToDiscordLinkData>>(linkDataConfig), new DebugOptions(this, linkDataConfig.GetType().Name));

            if (LinkData == null) LinkData = new List<GitToDiscordLinkData>();
        }

        private async Task<string> LoadConfig(string config) => await DataGrabber.GrabFromConfigs(config);

        private T ConvertConfig<T>(string rawJson) => JsonConvert.DeserializeObject<T>(rawJson);
    }
}
