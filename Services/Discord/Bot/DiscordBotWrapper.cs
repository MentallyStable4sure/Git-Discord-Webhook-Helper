using System.Reflection;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using MentallyStable.GitlabHelper.Data.Database;

namespace MentallyStable.GitlabHelper.Services.Discord.Bot
{
    public class DiscordBotWrapper
    {
        public readonly DiscordClient Discord;

        public DiscordBotWrapper(DiscordConfig config)
        {
            var configSetup = new DiscordConfiguration()
            {
                Token = config.Token,
                TokenType = (TokenType)config.Type,
                AutoReconnect = config.AutoReconnect,
                Intents = DiscordIntents.AllUnprivileged
            };

            var services = new ServiceCollection()
                .AddSingleton<Random>()
                .BuildServiceProvider();

            var servicesConfig = new SlashCommandsConfiguration() { Services = services };

            Discord = new DiscordClient(configSetup);
            var slash = Discord.UseSlashCommands(servicesConfig);

            slash.RegisterCommands(Assembly.GetAssembly(typeof(Program)), config.CustomGuidID > 0 ? config.CustomGuidID : null);
        }

        public async Task Connect()
        {
            await Discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
