using MentallyStable.GitHelper.Data.Git;

namespace MentallyStable.GitHelper.Helpers
{
    public static class Converters
    {
        public static GitActionType ToGitAction(this string resposeAction)
        {
            switch (resposeAction)
            {
                case "merge_request":
                    return GitActionType.MergeRequest;

                case "comment":
                    return GitActionType.Comment;

                case "merged":
                    return GitActionType.Merged;

                case "closed":
                    return GitActionType.Closed;

                case "wip":
                    return GitActionType.WorkInProgress;

                case "":
                    return GitActionType.None;

                default:
                    return GitActionType.All;
            }
        }

        public static string ToParserValue(this GitActionType action)
        {
            switch (action)
            {
                case GitActionType.MergeRequest:
                    return "merge_request";

                case GitActionType.Comment:
                    return "comment";

                case GitActionType.Merged:
                    return "merged";

                case GitActionType.Closed:
                    return "closed";

                case GitActionType.WorkInProgress:
                    return "wip";

                case GitActionType.All:
                case GitActionType.None:
                default:
                    return string.Empty;
            }
        }

        public static string ToImage(this GitActionType action)
        {
            switch (action)
            {
                case GitActionType.All:
                    return "https://bunbun.cloud/admin/funkymonke/img/pushchanges.png";

                case GitActionType.MergeRequest:
                    return "https://bunbun.cloud/admin/funkymonke/img/prcreated.png";

                case GitActionType.Comment:
                    return "https://bunbun.cloud/admin/funkymonke/img/prcomment.png";

                case GitActionType.Merged:
                    return "https://bunbun.cloud/admin/funkymonke/img/prmerged.png";

                case GitActionType.Closed:
                    return "https://bunbun.cloud/admin/funkymonke/img/prclosed.png";

                case GitActionType.None:
                default:
                    return string.Empty;
            }
        }
    }
}
