namespace MentallyStable.GitHelper.Data.Git.Gitlab
{
    public class GitlabAttributes
    {
        public int AuthorId = 0;

        public string CreatedAt = string.Empty;
        public string Description = string.Empty;

        public int Id = 0;
        public int Iid = 0;

        public string LastEditedAt = string.Empty;
        public int? LastEditedById = 0;

        public string MergeCommitSha = string.Empty;
        public string MergeError = string.Empty;
        public string MergeStatus = string.Empty;
        public string SourceBranch = string.Empty;
        public int SourceProjectId = 0;
        public int StateId = 0;

        public string TargetBranch = string.Empty;
        public int TargetProjectId = 0;
        public string Title = string.Empty;
        public string UpdatedAt = string.Empty;
        public int? UpdatedById = 0;

        public string Url = string.Empty;
        public GitlabRepository Source = new GitlabRepository();
        public GitlabRepository Target = new GitlabRepository();

        public GitlabCommit LastCommit = new GitlabCommit();

        public bool WorkInProgress = true;
        public string State = string.Empty;
    }
}
