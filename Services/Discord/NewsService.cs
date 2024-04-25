using DSharpPlus.Entities;
using MentallyStable.GitlabHelper.Extensions;

namespace MentallyStable.GitlabHelper.Services.Discord
{
    public class NewsService
    {
        public const string FULL_NEWS_IMAGE = "http://93.127.202.154/img/news.png";
        public const string SHORT_NEWS_IMAGE = "http://93.127.202.154/img/news-short.png";

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
    }
}
