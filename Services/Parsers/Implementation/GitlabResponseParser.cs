
using MentallyStable.GitHelper.Data.Git.Gitlab;

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
    }
}
