using MentallyStable.GitHelper.Data.Git.Gitlab;

namespace MentallyStable.GitHelper.Helpers
{
    public interface IGitHelper
    {
        public string GitHelperType { get; }

        public Task<string> ShowAccordingToType(string type, GitlabResponse response);
    }
}
