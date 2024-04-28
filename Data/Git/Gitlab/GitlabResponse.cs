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

        [JsonProperty("object_attributes")]
        public GitlabAttributes ObjectAttributes = new GitlabAttributes();
        [JsonProperty("merge_request")]
        public GitlabMergeRequest MergeRequest = new GitlabMergeRequest();
        public GitlabRepository Reposity = new GitlabRepository();
        public GitlabUser Author = new GitlabUser();
    }
}
