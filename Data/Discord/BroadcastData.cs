
using Newtonsoft.Json;
using DSharpPlus.Entities;

namespace MentallyStable.GitHelper.Data.Discord
{
    public class BroadcastData
    {
        public ulong ChannelID;
        public string[] PrefixesToTrack = new string[1] { "merge_request" };

        [JsonIgnore]
        public DiscordChannel DiscodChannelReference = null;
    }
}
