using Newtonsoft.Json;

namespace MentallyStable.GitHelper.Data.Git.Gitlab
{
    public class GitlabProject
    {
        public int Id = 0;
        public string Name = string.Empty;
        public string Description = string.Empty;

        [JsonProperty("web_url")]
        public string WebUrl = string.Empty;

        [JsonProperty("avatar_url")]
        public string AvatarUrl = string.Empty;
        public string Namespace = string.Empty;

        [JsonProperty("visibility_level")]
        public int VisibilityLevel = 0;

        [JsonProperty("path_with_namespace")]
        public string PathWithNamespace = string.Empty;

        [JsonProperty("default_branch")]
        public string DefaultBranch = string.Empty;

        [JsonProperty("ci_config_path")]
        public string CiConfigPath = string.Empty;
        public string Homepage = string.Empty;
        public string Url = string.Empty;

        [JsonProperty("ssh_url")]
        public string SshUrl = string.Empty;

        [JsonProperty("http_url")]
        public string HttpUrl = string.Empty;
    }
}
