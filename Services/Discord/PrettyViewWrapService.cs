using DSharpPlus.Entities;
using MentallyStable.GitHelper.Helpers;
using MentallyStable.GitHelper.Data.Database;
using MentallyStable.GitHelper.Data.Git.Gitlab;
using DSharpPlus;
using MentallyStable.GitHelper.Services.Parsers;

namespace MentallyStable.GitHelper.Services.Discord
{
    public class PrettyViewWrapService : IService
    {
        private readonly UserLinkEstablisherService _establisherService;
        private readonly DiscordClient _client;
        private readonly IResponseParser<GitlabResponse> _parser;

        public PrettyViewWrapService(UserLinkEstablisherService establisherService,
            DiscordClient discordClient, IResponseParser<GitlabResponse> parser)
        {
            _establisherService = establisherService;
            _client = discordClient;
            _parser = parser;
        }

        public Task InitializeService() => Task.CompletedTask;

        public async Task<DiscordMessageBuilder> WrapResponseInEmbed(GitlabResponse response, string descriptor, string[] lookupKeys)
        {
            string[] identifiers = response.CreateIdentifiers();
            string avatar = await CheckAvatarBasedOnLink(response, identifiers);
            string description = GetDescriptionBasedOnDescriptor(descriptor, response);
            description = await _parser.ParseLinks(_client, description, _establisherService);

            return new DiscordMessageBuilder()
                .WithEmbed(new DiscordEmbedBuilder()
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor()
                    {
                        Name = response.User.Username,
                        IconUrl = avatar
                    },

                    ImageUrl = response.ObjectKind.ToImage(response),
                    Color = DiscordColor.Black,
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail()
                    {
                        Url = avatar,
                        Width = 25,
                        Height = 25
                    },

                    Title = lookupKeys.ToTitle(), //response.ObjectAttributes.Title,
                    Description = description,
                    Url = response.ObjectAttributes.Url
                });
        }

        private async Task<string> CheckAvatarBasedOnLink(GitlabResponse response, string[] identifiers)
        {
            var connection = _establisherService.GetConnection(identifiers);
            if (connection == null) return response.User.AvatarUrl;
            else
            {
                var user = await _client.GetUserAsync(connection.DiscordSnowflakeId);
                return user.AvatarUrl;
            }
        }

        private static string GetDescriptionBasedOnDescriptor(string descriptor, GitlabResponse response)
        {
            string author = response.ObjectAttributes.LastCommit.Author.Name;
            if (string.IsNullOrEmpty(author)) author = response.MergeRequest.LastCommit.Author.Name;
            if (string.IsNullOrEmpty(author)) author = response.User.Name;

            if (descriptor == Endpoints.GITLAB_COMMENT_ATTRIBUTE)
            {
                return $"✨ [{response.Project.PathWithNamespace}] ✨\n__Author:__ ** {author} **\n\n> **{response.User.Name}** commented: \n\n`✏️ {response.ObjectAttributes.Note}`";
            }
            else return $"✨ [{response.Project.PathWithNamespace}] ✨\n__Author:__ ** {author} **\n\n> __Target:__ ** {response.ObjectAttributes.TargetBranch} **\n> __Source:__ ** {response.ObjectAttributes.SourceBranch} **\n\n`✏ {response.ObjectAttributes.Description}`";
        }
    }
}
