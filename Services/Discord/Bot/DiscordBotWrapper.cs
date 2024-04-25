using System.Reflection;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using MentallyStable.GitlabHelper.Data.Database;

namespace MentallyStable.GitlabHelper.Services.Discord.Bot
{
    public class DiscordBotWrapper
    {
        public DiscordBotWrapper(DiscordConfig config)
        {
            var configSetup = new DiscordConfiguration()
            {
                Token = config.Token,
                TokenType = (TokenType)config.Type,
                AutoReconnect = config.AutoReconnect,
                Intents = DiscordIntents.All
            };

            var services = new ServiceCollection()
                .AddSingleton<Random>()
                .BuildServiceProvider();

            var servicesConfig = new SlashCommandsConfiguration() { Services = services };

            var discord = new DiscordClient(configSetup);
            var slash = discord.UseSlashCommands(servicesConfig);

            slash.RegisterCommands(Assembly.GetAssembly(typeof(Program)), config.CustomGuidID > 0 ? config.CustomGuidID : null) ;
        }
    }
}
