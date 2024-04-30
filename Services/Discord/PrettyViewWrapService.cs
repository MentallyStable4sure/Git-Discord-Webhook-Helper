using DSharpPlus;
using DSharpPlus.Entities;
using MentallyStable.GitHelper.Helpers;
using MentallyStable.GitHelper.Data.Database;
using MentallyStable.GitHelper.Helpers.Gitlab;
using MentallyStable.GitHelper.Data.Git.Gitlab;
using MentallyStable.GitHelper.Services.Parsers;

namespace MentallyStable.GitHelper.Services.Discord
{
    public class PrettyViewWrapService : IService
    {
        private readonly UserLinkEstablisherService _establisherService;
        private readonly DiscordClient _client;
        private readonly IResponseParser<GitlabResponse> _parser;

        private readonly BaseGitHelper[] _gitlabHelpers;

        public PrettyViewWrapService(UserLinkEstablisherService establisherService,
            DiscordClient discordClient, IResponseParser<GitlabResponse> parser)
        {
            _establisherService = establisherService;
            _client = discordClient;
            _parser = parser;

            _gitlabHelpers = new BaseGitHelper[] {
                new GitlabMergeRequestHelper(Endpoints.GITLAB_MERGE_REQUEST_ATTRIBUTE, _client, _parser, _establisherService),
                new GitlabCommentHelper(Endpoints.GITLAB_COMMENT_ATTRIBUTE, _client, _parser, _establisherService),
                new GitlabCommitHelper(Endpoints.GITLAB_PUSH_ATTRIBUTE, _client, _parser, _establisherService)
            };
        }

        public Task InitializeService() => Task.CompletedTask;

        public async Task<DiscordMessageBuilder> WrapResponseInEmbed(GitlabResponse response, string descriptor, string[] lookupKeys)
        {
            string[] identifiers = response.CreateIdentifiers();
            string avatar = await CheckAvatarBasedOnLink(response, identifiers);
            string description = await GetDescriptionBasedOnDescriptor(descriptor, response);

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

        private async Task<string> GetDescriptionBasedOnDescriptor(string descriptor, GitlabResponse response)
        {
            string info = string.Empty;
            foreach (var helper in _gitlabHelpers)
            {
                info = await helper.ShowAccordingToType(descriptor, response);
                if (string.IsNullOrEmpty(info)) continue;
                else break;
            }

            return info;
        }
    }
}
