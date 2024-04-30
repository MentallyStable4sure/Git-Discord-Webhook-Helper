using MentallyStable.GitHelper.Data.Git.Gitlab;

namespace MentallyStable.GitHelper.Helpers
{
    public abstract class BaseGitHelper : IGitHelper
    {
        public string GitHelperType { get; private set; }

        public BaseGitHelper(string gitHelperType)
        {
            this.GitHelperType = gitHelperType.ToLower();
        }

        public async Task<string> ShowAccordingToType(string type, GitlabResponse response)
        {
            if (type.ToLower() != GitHelperType) return string.Empty;

            return await OnShow(response);
        }

        protected abstract Task<string> OnShow(GitlabResponse text);
    }
}
