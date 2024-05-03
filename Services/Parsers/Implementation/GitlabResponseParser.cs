
using DSharpPlus;
using MentallyStable.GitHelper.Data.Git.Gitlab;
using MentallyStable.GitHelper.Services.Discord;

namespace MentallyStable.GitHelper.Services.Parsers.Implementation
{
    public class GitlabResponseParser : IService, IResponseParser<GitlabResponse>
    {
        public Task InitializeService() => Task.CompletedTask;

        public string[] ParsePrefixes(GitlabResponse response, string[] prefixes)
        {
            List<string> prefixesFound = new List<string>();
            string[] listToLookup =
            {
                response.ObjectAttributes.Title,
                response.ObjectAttributes.SourceBranch,
                response.ObjectKind
            };

            foreach (string stringToLookup in listToLookup)
            {
                foreach (var prefix in prefixes)
                {
                    if (!stringToLookup.ToLower().Contains(prefix)) continue;
                    prefixesFound.Add(prefix);
                }
            }

            return prefixesFound.ToArray();
        }

        public async Task<string> ParseLinks(DiscordClient client, string description, UserLinkEstablisherService establisher)
        {
            if (!description.Contains('@')) return description;

            string[] divided = description.Split('@', ' ');

            foreach (var item in divided)
            {
                Console.WriteLine(item);
            }

            for (int i = 0; i < divided.Length; i++)
            {
                var link = establisher.GetConnection(divided[i]);
                if (link == null) continue;
                var user = await client.GetUserAsync(link.DiscordSnowflakeId);
                divided[i] = $"`{user.Mention}`";
            }

            description = string.Empty;
            foreach (var word in divided)
            {
                description += $"{word} ";
            }


            description = description.Replace('#', ' '); //Removing headings

            return description;
        }
    }
}
