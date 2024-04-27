
using MentallyStable.GitHelper.Data.Database;
using MentallyStable.GitHelper.Data.Git.Gitlab;

namespace MentallyStable.GitHelper.Helpers
{
    public static class GitlabConverterHelper
    {
        public static string ToImage(this string resposeAction, GitlabResponse actionDiffer)
        {
            switch (resposeAction.ToLower())
            {
                case "":
                    return string.Empty;

                case Endpoints.GITLAB_MERGE_REQUEST_ATTRIBUTE:
                    return CheckMergeRequestState(actionDiffer.ObjectAttributes.State.ToLower());

                case Endpoints.GITLAB_COMMENT_ATTRIBUTE:
                    return CheckCommentAppliedTo(actionDiffer.ObjectAttributes.NoteableType.ToLower());

                default:
                    return "https://bunbun.cloud/admin/funkymonke/img/_drip_monkey_banner.gif";
            }
        }

        public static string[] ToLookupKeys(this string objectKind, GitlabResponse response)
        {
            switch (objectKind.ToLower())
            {
                case Endpoints.GITLAB_COMMENT_ATTRIBUTE:
                    return new[]
                    {
                        response.ObjectAttributes.Title,
                        response.ObjectAttributes.SourceBranch,
                        response.ObjectAttributes.Note,
                        response.ObjectAttributes.LastCommit.Message
                    };

                case Endpoints.GITLAB_MERGE_REQUEST_ATTRIBUTE:
                default:
                    return new[]
                    {
                        response.ObjectAttributes.Title,
                        response.ObjectAttributes.SourceBranch
                    };
            }
        }

        public static string ToTitle(this string[] lookupKeys)
        {
            string title = string.Empty;
            for (int i = 0; i < lookupKeys.Length; i++)
            {
                title = lookupKeys[i];
                if (!string.IsNullOrEmpty(title)) return title;
            }

            return string.Empty;
        }

        private static string CheckCommentAppliedTo(string actionDiffer)
        {
            if (actionDiffer == Endpoints.GITLAB_COMMENT_PR_TYPE.ToLower()) return "https://bunbun.cloud/admin/funkymonke/img/prcomment.png";
            else if (actionDiffer == Endpoints.GITLAB_COMMENT_COMMIT_TYPE.ToLower()) return "https://bunbun.cloud/admin/funkymonke/img/commitcommented.png";
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
