using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using MentallyStable.GitHelper.Extensions;
using MentallyStable.GitHelper.Services.Discord;

namespace MentallyStable.GitHelper.Commands
{
    public class NewsCommand : ApplicationCommandModule
    {
        [SlashCommand("news", "A slash command made to test the DSharpPlusSlashCommands library and paste some news")]
        public async Task TellNews(InteractionContext ctx, [Option("Title", "Clickable title of embed message")] string newsTitle, [Option("Description", "Long message")]  string newsDescription, [Option("URL", "URL where title points")] string url = "")
        {
            var user = ctx.User;
            var author = new DiscordEmbedBuilder.EmbedAuthor()
            {
                IconUrl = user.AvatarUrl,
                Name = user.Username
            };

            var builder = new DiscordInteractionResponseBuilder()
                .GetEmbedTemplate(
                newsTitle, 
                newsDescription,
                NewsService.FULL_NEWS_IMAGE,
                url, author);
            await ctx.CreateResponseAsync(builder);
        }

        [SlashCommand("news-short", "Short version of /news")]
        public async Task TellNewsShortly(InteractionContext ctx, [Option("Description", "Long message")] string newsDescription, [Option("URL", "URL where title points")] string url = "")
        {
            var builder = NewsService.CreateBuilderNews(null, newsDescription, url, NewsService.SHORT_NEWS_IMAGE);
            await ctx.CreateResponseAsync(builder);
        }
    }
}
