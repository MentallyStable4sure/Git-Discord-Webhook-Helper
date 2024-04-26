namespace MentallyStable.GitHelper.Data.Git
{
    public enum GitActionType
    {
        None = 0,
        All = 1,
        PullRequestCreation = 2,
        PullRequestMerging = 3,
        PullRequestClosing = 4,
        PushAction = 5,
        Comment = 6
    }
}
