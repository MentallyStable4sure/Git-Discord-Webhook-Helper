using DSharpPlus;
using MentallyStable.GitHelper.Data.Git.Gitlab;
using MentallyStable.GitHelper.Services.Discord;
using MentallyStable.GitHelper.Services.Parsers;

namespace MentallyStable.GitHelper.Helpers.Gitlab
{
    public class GitlabCommentHelper : BaseGitHelper
    {
        private readonly UserLinkEstablisherService _establisherService;
        private readonly DiscordClient _client;
        private readonly IResponseParser<GitlabResponse> _parser;

        public GitlabCommentHelper(string gitHelperType, DiscordClient client,
            IResponseParser<GitlabResponse> parse, UserLinkEstablisherService establisher) : base(gitHelperType)
        {
            _client = client;
            _establisherService = establisher;
            _parser = parse;
        }

        protected override async Task<string> OnShow(GitlabResponse response)
        {
            string author = response.ObjectAttributes.LastCommit.Author.Name;
            if (string.IsNullOrEmpty(author)) author = response.MergeRequest.LastCommit.Author.Name;
            if (string.IsNullOrEmpty(author)) author = response.User.Name;

            var note = await _parser.ParseLinks(_client, response.ObjectAttributes.Note, _establisherService);
            return $"✨ ** {response.Project.PathWithNamespace} ** ✨\n📌 __Author:__ ** {author} **\n\n> **{response.User.Name}** commented: \n `{note}`";
        }
    }
}
