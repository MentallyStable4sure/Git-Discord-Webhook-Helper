using DSharpPlus;
using MentallyStable.GitlabHelper.Services;
using MentallyStable.GitlabHelper.Data.Database;
using MentallyStable.GitlabHelper.Services.Discord;
using MentallyStable.GitlabHelper.Services.Development;
using MentallyStable.GitlabHelper.Services.Discord.Bot;

namespace MentallyStable.GitlabHelper.Registrators
{
    public sealed class SingleInstanceRegistrator : IRegistrator
    {
        private readonly IDebugger _debugger;
        private readonly DiscordConfig _discordConfig;
        private readonly DiscordClient _discordClient;
        private readonly List<IService> _services;

        public SingleInstanceRegistrator(ConfigsRegistrator configs)
        {
            _debugger = new Debugger();
            _discordConfig = configs.DiscordConfig;

            var configSetup = ConvertDiscordConfig(_discordConfig);
            _discordClient = new DiscordClient(configSetup);

            _services = new List<IService>()
            {
                new NewsService(),
                new BroadcastDataService(configs, _discordClient)
            };
        }

        public async Task Register(WebApplicationBuilder builder)
        {
            await RegisterCustomServices(_services);

            DiscordBotWrapper discordBot = null;

            _debugger.TryExecute(() => discordBot = new DiscordBotWrapper(_discordClient, _discordConfig));

            builder.Services.AddSingleton<DiscordBotWrapper>(discordBot);

            StartBot(discordBot);
        }

        private void StartBot(DiscordBotWrapper discordWrapper) => discordWrapper.Connect().ConfigureAwait(false);

        private async Task RegisterCustomServices(List<IService> services)
        {
            foreach (var service in services)
            {
                await service.InitializeService();
            }
        }

        private DiscordConfiguration ConvertDiscordConfig(DiscordConfig config)
        {
            return new DiscordConfiguration()
            {
                Token = config.Token,
                TokenType = (TokenType)config.Type,
                AutoReconnect = config.AutoReconnect,
                Intents = DiscordIntents.AllUnprivileged
            };
        }
    }
}
