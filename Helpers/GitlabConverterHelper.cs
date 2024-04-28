
using MentallyStable.GitHelper.Data.Database;
using MentallyStable.GitHelper.Data.Git.Gitlab;

namespace MentallyStable.GitHelper.Helpers
{
    public static class GitlabConverterHelper
    {
        public static string[] CreateIdentifiers(this GitlabResponse response)
        {
            return new string[3]
            {
                response.User.Username,
                response.User.Email,
                response.User.Name
            };
        }

        public static string ToImage(this string resposeAction, GitlabResponse actionDiffer)
        {
            switch (resposeAction.ToLower())
            {
                case "":
                    return string.Empty;

                case Endpoints.GITLAB_MERGE_REQUEST_ATTRIBUTE:
                    return CheckMergeRequestState(actionDiffer.ObjectAttributes.State.ToLower(), actionDiffer.ObjectAttributes.Action);

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
                        response.MergeRequest.SourceBranch,
                        response.MergeRequest.Title,
                        response.ObjectAttributes.Source.Name,
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

        public static string[] ToLookupKeysLowered(this string objectKind, GitlabResponse response)
        {
            string[] keys = objectKind.ToLookupKeys(response);
            for (int i = 0; i < keys.Length; i++)
            {
                keys[i] = keys[i].ToLower();
            }

            return keys;
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
            if (actionDiffer == Endpoints.GITLAB_COMMENT_PR_TYPE.ToLower()) return "https://bunbun.cloud/admin/funkymonke/img/prcommentking.png";
            else if (actionDiffer == Endpoints.GITLAB_COMMENT_COMMIT_TYPE.ToLower()) return "https://bunbun.cloud/admin/funkymonke/img/commitcommented.png";
            else return "https://bunbun.cloud/admin/funkymonke/img/_drip_monkey_banner.gif";
        }

        private static string CheckMergeRequestState(string actionDiffer, string prAction)
        {
            if(prAction != null && prAction.Length > 0 && prAction == "update") return "https://bunbun.cloud/admin/funkymonke/img/prupdated.png";
            if (actionDiffer == "opened") return "https://bunbun.cloud/admin/funkymonke/img/prcreated.png";
            else if (actionDiffer == "closed") return "https://bunbun.cloud/admin/funkymonke/img/prclosed.png";
            else return "https://bunbun.cloud/admin/funkymonke/img/prmerged.png";
        }
    }
}
