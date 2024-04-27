using Newtonsoft.Json;

namespace MentallyStable.GitHelper.Data.Git.Gitlab
{
    public class GitlabAttributes
    {
        [JsonProperty("asignee_id")]
        public int AsigneeId = 0;
        [JsonProperty("author_id")]
        public int AuthorId = 0;

        [JsonProperty("created_at")]
        public string CreatedAt = string.Empty;
        public string Description = string.Empty;
        public string Note = string.Empty;
        [JsonProperty("noteable_type")]
        public string NoteableType = string.Empty;

        public int Id = 0;
        public int Iid = 0;

        [JsonProperty("last_edited_at")]
        public string LastEditedAt = string.Empty;
        [JsonProperty("last_edited_by_id")]
        public int? LastEditedById = 0;

        [JsonProperty("line_code")]
        public string LineCode = string.Empty;
        [JsonProperty("merge_commit_sha")]
        public string MergeCommitSha = string.Empty;
        [JsonProperty("merge_error")]
        public string MergeError = string.Empty;
        [JsonProperty("merge_status")]
        public string MergeStatus = string.Empty;
        [JsonProperty("source_branch")]
        public string SourceBranch = string.Empty;
        [JsonProperty("source_project_id")]
        public int SourceProjectId = 0;
        [JsonProperty("state_id")]
        public int StateId = 0;

        [JsonProperty("target_branch")]
        public string TargetBranch = string.Empty;
        [JsonProperty("target_project_id")]
        public int TargetProjectId = 0;
        public string Title = string.Empty;
        [JsonProperty("updated_at")]
        public string UpdatedAt = string.Empty;
        [JsonProperty("updated_by_id")]
        public int? UpdatedById = 0;

        public string Url = string.Empty;
        public GitlabRepository Source = new GitlabRepository();
        public GitlabRepository Target = new GitlabRepository();

        public GitlabCommit Commit = new GitlabCommit();
        [JsonProperty("last_commit")]
        public GitlabCommit LastCommit = new GitlabCommit();

        [JsonProperty("work_in_progress")]
        public bool WorkInProgress = true;
        public string State = string.Empty;
        public string Action = string.Empty;
    }
}
