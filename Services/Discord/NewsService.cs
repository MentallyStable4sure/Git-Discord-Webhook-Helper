using DSharpPlus.Entities;
using MentallyStable.GitHelper.Extensions;

namespace MentallyStable.GitHelper.Services.Discord
{
    public class NewsService : IService
    {
        public const string FULL_NEWS_IMAGE = "https://bunbun.cloud/admin/funkymonke/img/news.png";
        public const string SHORT_NEWS_IMAGE = "https://bunbun.cloud/admin/funkymonke/img/_drip_monkey_banner.gif";

        public static DiscordInteractionResponseBuilder CreateBuilderNews(
            string title, string description, string url = null, string imageUrl = FULL_NEWS_IMAGE, DiscordEmbedBuilder.EmbedAuthor author = null)
        {
            return new DiscordInteractionResponseBuilder()
                .GetEmbedTemplate(
                title,
                description,
                imageUrl,
                url, author);
        }

        public Task InitializeService() => Task.CompletedTask;
    }
}
