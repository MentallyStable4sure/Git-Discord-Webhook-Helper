namespace MentallyStable.GitHelper.Data.Git.Gitlab
{
    public class GitlabCommit
    {
        public string Id = string.Empty;
        public string Message = string.Empty;
        public string Title = string.Empty;
        public string Timestamp = string.Empty;
        public string Url = string.Empty;

        public GitlabAuthor Author = new GitlabAuthor();
    }
}
