
using Newtonsoft.Json;
using DSharpPlus.Entities;
using MentallyStable.GitlabHelper.Data.Git;

namespace MentallyStable.GitlabHelper.Data.Discord
{
    public class BroadcastData
    {
        public ulong ChannelID;
        public string[] PrefixesToTrack = new string[0];
        public GitActionType[] ActionsToTrack = new GitActionType[1] { GitActionType.All };

        [JsonIgnore]
        public DiscordChannel DiscodChannelReference = null;
    }
}
