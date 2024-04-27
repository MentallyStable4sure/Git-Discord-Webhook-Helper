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
                Color = DiscordColor.Black,
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail()
                {
                    Url = author == null ? null : author.IconUrl,
                    Height = 25,
                    Width = 25
                }
            });
        }

        public static DiscordInteractionResponseBuilder GetInfoEmbed(this DiscordInteractionResponseBuilder builder,
            string title, string description, string imageUrl = "https://bunbun.cloud/admin/funkymonke/img/monke_roll.gif", string url = "")
        {
            return builder.AddEmbed(new DiscordEmbedBuilder()
            {
                ImageUrl = imageUrl,
                Url = url,
                Title = title,
                Description = description,
                Author = null,
                Color = DiscordColor.Orange,
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail()
                {
                    Url = "https://bunbun.cloud/admin/funkymonke/img/_monkey_banner.gif",
                    Height = 25,
                    Width = 25
                }
            });
        }
    }
}
