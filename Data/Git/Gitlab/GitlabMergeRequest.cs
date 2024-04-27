using Newtonsoft.Json;

namespace MentallyStable.GitHelper.Data.Git.Gitlab
{
    public class GitlabMergeRequest
    {
        public int Id = 0;
        public int Iid = 0;
        public string Title = string.Empty;

        [JsonProperty("source_branch")]
        public string SourceBranch = string.Empty;
        [JsonProperty("target_branch")]
        public string TargetBranch = string.Empty;
        public string State = string.Empty;
        [JsonProperty("merge_status")]
        public string MergeStatus = string.Empty;
        public string Url = string.Empty;
        [JsonProperty("last_commit")]
        public GitlabCommit LastCommit = new GitlabCommit();
    }
}
