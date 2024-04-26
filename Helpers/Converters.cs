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

                default:
                    return GitActionType.All;
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

                default:
                    return string.Empty;
            }
        }
    }
}
