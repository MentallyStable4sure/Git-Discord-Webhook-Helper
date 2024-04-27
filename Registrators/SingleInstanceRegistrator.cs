using DSharpPlus;
using MentallyStable.GitHelper.Services;
using MentallyStable.GitHelper.Data.Database;
using MentallyStable.GitHelper.Data.Development;
using MentallyStable.GitHelper.Services.Discord;
using MentallyStable.GitHelper.Services.Development;
using MentallyStable.GitHelper.Services.Discord.Bot;
using System.Net;
using System;

namespace MentallyStable.GitHelper.Registrators
{
    public sealed class SingleInstanceRegistrator : IRegistrator
    {
        private readonly IDebugger _debugger;
        private readonly DiscordConfig _discordConfig;
        private readonly DiscordClient _discordClient;
        private readonly List<IService> _services;

        private readonly BroadcastDataService _broadcastDataService;

        public SingleInstanceRegistrator(ConfigsRegistrator configs)
        {
            _debugger = new Debugger();
            _discordConfig = configs.DiscordConfig;

            var configSetup = ConvertDiscordConfig(_discordConfig);
            _discordClient = new DiscordClient(configSetup);

            _broadcastDataService = new BroadcastDataService(configs, _discordClient);

            _services = new List<IService>()
            {
                _broadcastDataService,
            };
        }

        public async Task Register(WebApplicationBuilder builder)
        {
            await InitializeCustomServices(_services);

            DiscordBotWrapper discordBot = null;

            _debugger.TryExecute(() => discordBot = new DiscordBotWrapper(_discordClient, _discordConfig), new DebugOptions(this, typeof(DiscordBotWrapper).Name));

            builder.Services.AddSingleton<DiscordBotWrapper>(discordBot);
            builder.Services.AddSingleton<BroadcastDataService>(_broadcastDataService);

            StartBot(discordBot);
        }

        private void StartBot(DiscordBotWrapper discordWrapper)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Prefixes tracked: ");
            foreach (var item in _broadcastDataService.GetAllPrefixes())
            {
                Console.Write($"{item}; ");
            }
            Console.WriteLine("\n\n");
            Console.ResetColor();

            discordWrapper.Connect().ConfigureAwait(false);
        }

        private async Task InitializeCustomServices(List<IService> services)
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
