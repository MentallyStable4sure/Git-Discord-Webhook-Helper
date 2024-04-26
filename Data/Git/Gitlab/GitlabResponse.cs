using Newtonsoft.Json;

namespace MentallyStable.GitHelper.Data.Git.Gitlab
{
    public class GitlabResponse
    {
        [JsonProperty("object_kind")]
        public string ObjectKind = string.Empty;

        [JsonProperty("event_type")]
        public string EventType = string.Empty;

        public GitlabUser User = new GitlabUser();
        public GitlabProject Project = new GitlabProject();
        public GitlabRepository Reposity = new GitlabRepository();

    }
}
