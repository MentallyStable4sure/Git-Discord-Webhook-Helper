using DSharpPlus;
using MentallyStable.GitHelper.Services;
using MentallyStable.GitHelper.Data.Database;
using MentallyStable.GitHelper.Data.Development;
using MentallyStable.GitHelper.Services.Discord;
using MentallyStable.GitHelper.Services.Development;
using MentallyStable.GitHelper.Services.Discord.Bot;
using MentallyStable.GitHelper.Services.Parsers.Implementation;
using MentallyStable.GitHelper.Helpers;

namespace MentallyStable.GitHelper.Registrators
{
    public sealed class SingleInstanceRegistrator : IRegistrator
    {
        private readonly IDebugger _debugger;
        private readonly DiscordConfig _discordConfig;
        private readonly DiscordClient _discordClient;
        private readonly List<IService> _services;

        private readonly BroadcastDataService _broadcastDataService;
        private readonly TrackingService _trackingService;
        private readonly UserLinkEstablisherService _userLinkEstablisherService;
        private readonly PrettyViewWrapService _prettyViewWrapService;
        private readonly ThreadWatcherService _threadWatcherService;
        private readonly GitCatcherHelper _catcherHelper;

        public SingleInstanceRegistrator(ConfigsRegistrator configs)
        {
            _debugger = new Debugger();
            _discordConfig = configs.DiscordConfig;

            var configSetup = ConvertDiscordConfig(_discordConfig);
            _discordClient = new DiscordClient(configSetup);

            _broadcastDataService = new BroadcastDataService(configs.BroadcastData, _discordClient);
            _trackingService = new TrackingService(_discordClient, configs.BroadcastData);
            _userLinkEstablisherService = new UserLinkEstablisherService(configs.LinkData);
            _prettyViewWrapService = new PrettyViewWrapService(_userLinkEstablisherService, _discordClient, new GitlabResponseParser());
            _threadWatcherService = new ThreadWatcherService(_userLinkEstablisherService, _discordClient);
            _catcherHelper = new GitCatcherHelper(_threadWatcherService, _broadcastDataService);


            _services = new List<IService>()
            {
                _broadcastDataService,
                _trackingService,
                _userLinkEstablisherService,
                _prettyViewWrapService,
                _threadWatcherService,
                _catcherHelper
            };
        }

        public async Task Register(WebApplicationBuilder builder)
        {
            var servicesProvider = await InitializeCustomServices(_services);

            DiscordBotWrapper discordBot = null;

            _debugger.TryExecute(() => discordBot = new DiscordBotWrapper(_discordClient, _discordConfig, servicesProvider), new DebugOptions(this, typeof(DiscordBotWrapper).Name));

            builder.Services.AddSingleton<DiscordBotWrapper>(discordBot);
            builder.Services.AddSingleton<BroadcastDataService>(_broadcastDataService);
            builder.Services.AddSingleton<TrackingService>(_trackingService);
            builder.Services.AddSingleton<PrettyViewWrapService>(_prettyViewWrapService);
            builder.Services.AddSingleton<UserLinkEstablisherService>(_userLinkEstablisherService);
            builder.Services.AddSingleton<ThreadWatcherService>(_threadWatcherService);
            builder.Services.AddSingleton<GitCatcherHelper>(_catcherHelper);

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

        private async Task<ServiceProvider> InitializeCustomServices(List<IService> services)
        {
            ServiceCollection collection = new ServiceCollection();
            foreach (var service in services)
            {
                await service.InitializeService();
                collection.AddSingleton(service.GetType(), service);
            }
            return collection.BuildServiceProvider();
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
