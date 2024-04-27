
using MentallyStable.GitHelper.Data.Git.Gitlab;

namespace MentallyStable.GitHelper.Helpers
{
    public static class GitlabConverterHelper
    {
        public static string ToImage(this string resposeAction, GitlabResponse actionDiffer)
        {
            switch (resposeAction)
            {
                case "":
                    return string.Empty;

                case "merge_request":
                    return CheckMergeRequestState(actionDiffer.ObjectAttributes.State.ToLower());

                case "note":
                    return CheckCommentAppliedTo(actionDiffer.ObjectAttributes.NoteableType.ToLower());

                default:
                    return "https://bunbun.cloud/admin/funkymonke/img/_drip_monkey_banner.gif";
            }
        }

        private static string CheckCommentAppliedTo(string actionDiffer)
        {
            if (actionDiffer == "mergerequest") return "https://bunbun.cloud/admin/funkymonke/img/prcomment.png";
            else if (actionDiffer == "commit") return "https://bunbun.cloud/admin/funkymonke/img/commitcommented.png";
            else return "https://bunbun.cloud/admin/funkymonke/img/_drip_monkey_banner.gif";
        }

        private static string CheckMergeRequestState(string actionDiffer)
        {
            if (actionDiffer == "opened") return "https://bunbun.cloud/admin/funkymonke/img/prcreated.png";
            else if (actionDiffer == "closed") return "https://bunbun.cloud/admin/funkymonke/img/prclosed.png";
            else return "https://bunbun.cloud/admin/funkymonke/img/prmerged.png";
        }
    }
}
