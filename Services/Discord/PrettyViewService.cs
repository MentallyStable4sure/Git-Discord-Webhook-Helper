using DSharpPlus.Entities;
using MentallyStable.GitHelper.Helpers;
using MentallyStable.GitHelper.Data.Git.Gitlab;

namespace MentallyStable.GitHelper.Services.Discord
{
    public class PrettyViewService : IService
    {
        public PrettyViewService()
        {

        }

        public async Task InitializeService()
        {
            
        }

        public static DiscordMessageBuilder WrapResponseInEmbed(GitlabResponse response)
        {
            return new DiscordMessageBuilder()
                .WithEmbed(new DiscordEmbedBuilder()
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor()
                    {
                        Name = response.User.Username,
                        IconUrl = response.User.AvatarUrl
                    },
                    ImageUrl = response.ActionEventType.ToImage(),
                    Color = DiscordColor.Black,
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail()
                    {
                        Url = response.User.AvatarUrl,
                        Width = 25,
                        Height = 25
                    },
                    Title = response.ObjectAttributes.Title,
                    Description = response.ObjectAttributes.Description,
                    Url = response.ObjectAttributes.Url
                });
        }
    }
}
