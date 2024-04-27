
using MentallyStable.GitHelper.Helpers;
using MentallyStable.GitHelper.Data.Git;
using MentallyStable.GitHelper.Data.Git.Gitlab;

namespace MentallyStable.GitHelper.Services.Parsers.Implementation
{
    public class GitlabResponseParser : IService, IResponseParser<GitlabResponse, GitActionType>
    {
        public Task InitializeService() => Task.CompletedTask;

        public string[] ParsePrefixes(GitlabResponse response, string[] prefixes)
        {
            List<string> prefixesFound = new List<string>();
            string[] listToLookup =
            {
                response.ObjectAttributes.Title,
                response.ObjectAttributes.SourceBranch
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

        public GitActionType[] ParseTypes(GitlabResponse response, GitActionType[] types)
        {
            if (types.Contains(GitActionType.All)) return new GitActionType[1] { GitActionType.All };

            List<GitActionType> typesFound = new List<GitActionType>();
            string[] listToLookup =
            {
                response.ObjectAttributes.State,
                response.ObjectAttributes.WorkInProgress ? "WIP" : string.Empty,
                response.EventType
            };

            foreach (string stringToLookup in listToLookup)
            {
                foreach (var type in types)
                {
                    if (!stringToLookup.ToLower().Contains(type.ToParserValue())) continue;
                    if (typesFound.Contains(type)) continue;
                    typesFound.Add(type);
                }
            }

            return typesFound.ToArray();
        }
    }
}
