using DSharpPlus.Entities;

namespace MentallyStable.GitHelper.Extensions
{
    public static class BotVisualsExtensions
    {
        public static DiscordInteractionResponseBuilder GetEmbedTemplate(this DiscordInteractionResponseBuilder builder, 
            string title, string description, string imageUrl, string url = "", DiscordEmbedBuilder.EmbedAuthor author = null)
        {
            return builder.AddEmbed(new DiscordEmbedBuilder()
            {
                ImageUrl = imageUrl,
                Url = url,
                Title = title,
                Description = description,
                Author = author,
                Color = DiscordColor.Black
            });
        }
    }
}
