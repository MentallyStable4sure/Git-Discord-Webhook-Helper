using System.Reflection;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using MentallyStable.GitHelper.Data.Database;

namespace MentallyStable.GitHelper.Services.Discord.Bot
{
    public class DiscordBotWrapper
    {
        public CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();

        public readonly DiscordClient Discord;

        public DiscordBotWrapper(DiscordClient client, DiscordConfig config)
        {
            //Injected additional classes for slash commands itself (not services, they use different scoped injection):

            var services = new ServiceCollection()
                .AddSingleton<Random>()
                .BuildServiceProvider();

            var servicesConfig = new SlashCommandsConfiguration() { Services = services };

            Discord = client;
            var slash = Discord.UseSlashCommands(servicesConfig);

            slash.RegisterCommands(Assembly.GetAssembly(typeof(Program)), config.CustomGuidID > 0 ? config.CustomGuidID : null);
        }

        public async Task Connect()
        {
            CancellationTokenSource = new CancellationTokenSource();

            await Discord.ConnectAsync();
            await Task.Delay(-1, CancellationTokenSource.Token);
        }

        public async Task Disconnect()
        {
            await Discord.DisconnectAsync();
            CancellationTokenSource?.Cancel();
            CancellationTokenSource?.Dispose();
        }
    }
}
