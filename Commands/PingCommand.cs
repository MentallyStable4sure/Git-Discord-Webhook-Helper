using DSharpPlus.SlashCommands;

namespace MentallyStable.GitlabHelper.Commands
{
    public class PingCommand : ApplicationCommandModule
    {
        [SlashCommand("Ping", "A slash command made to test the DSharpPlusSlashCommands library")]
        public async Task Ping(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.Pong);
        }
    }
}
