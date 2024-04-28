namespace MentallyStable.GitHelper.Data.Database
{
    public class Endpoints
    {
        //--------------CONFIGS--------------
        public const string DISCORD_CONFIG = "discordconfig.json";
        public const string DISCORD_BROADCASTERS_CONFIG = "discordbroadcasters.json";
        public const string ESTABLISHED_CONNECTIONS_CONFIG = "establishedconnections.json";


        //-----STATIC NAMINGS/ATTRIBUTES-----

        //GITLAB
        public const string GITLAB_MERGE_REQUEST_ATTRIBUTE = "merge_request";
        public const string GITLAB_COMMENT_ATTRIBUTE = "note";
        public const string GITLAB_PUSH_ATTRIBUTE = "push";
        public const string GITLAB_ISSUE_ATTRIBUTE = "issue";
        public const string GITLAB_TAG_PUSH_ATTRIBUTE = "tag_push";
        public const string GITLAB_WIKI_ATTRIBUTE = "wiki_page";
        public const string GITLAB_PIPELINE_ATTRIBUTE = "pipeline";
        public const string GITLAB_JOB_ATTRIBUTE = "build";


        public const string GITLAB_COMMENT_PR_TYPE = "MergeRequest";
        public const string GITLAB_COMMENT_COMMIT_TYPE = "Commit";
        public const string GITLAB_COMMENT_ISSUE_TYPE = "Issue";
        public const string GITLAB_COMMENT_SNIPPET_TYPE = "Snippet";

        public const string GITLAB_MERGE_STATUS_UNCHECKED_TYPE = "unchecked";
        public const string GITLAB_MERGE_STATUS_CAN_BE_MERGED_TYPE = "can_be_merged";
        public const string GITLAB_MERGE_STATUS_CANT_BE_MERGED_TYPE = "cannot_be_merged";


        //-----------------------------------
    }
}
