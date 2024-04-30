using DSharpPlus;
using MentallyStable.GitHelper.Data.Git.Gitlab;
using MentallyStable.GitHelper.Services.Discord;
using MentallyStable.GitHelper.Services.Parsers;

namespace MentallyStable.GitHelper.Helpers.Gitlab
{
    public class GitlabCommitHelper : BaseGitHelper
    {
        private readonly UserLinkEstablisherService _establisherService;
        private readonly DiscordClient _client;
        private readonly IResponseParser<GitlabResponse> _parser;

        public GitlabCommitHelper(string gitHelperType, DiscordClient client,
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

            var description = await _parser.ParseLinks(_client, response.ObjectAttributes.Description, _establisherService);
            string info = $"✨ ** {response.Project.PathWithNamespace} ** ✨\n📌 __Author:__ ** {author} **\n\n> 🎯 __Target:__ ** {response.ObjectAttributes.TargetBranch} **\n> 📦 __Source:__ ** {response.ObjectAttributes.SourceBranch} **\n `{description}` ";

            string lastCommit = string.IsNullOrEmpty(response.MergeRequest.LastCommit.Message) ? response.ObjectAttributes.LastCommit.Message : response.MergeRequest.LastCommit.Message;
            if (string.IsNullOrEmpty(lastCommit)) lastCommit = string.Empty;
            else
            {
                var parsedLinksLastCommit = await _parser.ParseLinks(_client, lastCommit, _establisherService);
                lastCommit = $"\n\n>>> 🚩 Last commit: {Truncate(parsedLinksLastCommit, 50)}";
            }

            info += lastCommit;
            return info;
        }

        public static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }
}
