using DSharpPlus.Entities;
using MentallyStable.GitHelper.Data.Database;
using MentallyStable.GitHelper.Data.Git.Gitlab;

namespace MentallyStable.GitHelper.Helpers
{
    public class PrettyViewHelper
    {

        public static DiscordMessageBuilder WrapResponseInEmbed(GitlabResponse response, string descriptor, string[] lookupKeys)
        {
            return new DiscordMessageBuilder()
                .WithEmbed(new DiscordEmbedBuilder()
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor()
                    {
                        Name = response.User.Username,
                        IconUrl = response.User.AvatarUrl
                    },

                    ImageUrl = response.ObjectKind.ToImage(response),
                    Color = DiscordColor.Black,
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail()
                    {
                        Url = response.User.AvatarUrl,
                        Width = 25,
                        Height = 25
                    },

                    Title = lookupKeys.ToTitle(), //response.ObjectAttributes.Title,
                    Description = GetDescriptionBasedOnDescriptor(descriptor, response),
                    Url = response.ObjectAttributes.Url
                });
        }

        private static string GetDescriptionBasedOnDescriptor(string descriptor, GitlabResponse response)
        {
            if (descriptor == Endpoints.GITLAB_COMMENT_ATTRIBUTE) return response.ObjectAttributes.Note;
            else return response.ObjectAttributes.Description;
        }
    }
}
