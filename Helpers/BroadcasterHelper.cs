
using Newtonsoft.Json;
using DSharpPlus.Entities;
using MentallyStable.GitHelper.Data;
using MentallyStable.GitHelper.Data.Discord;

namespace MentallyStable.GitHelper.Helpers
{
    public static class BroadcasterHelper
    {
        public static async Task BroadcastToAll(this List<DiscordChannel> discordChannels, DiscordMessageBuilder message)
        {
            if (discordChannels == null || discordChannels.Count <= 0) return;

            foreach (var channelToBroadcast in discordChannels)
            {
                await channelToBroadcast.SendMessageAsync(message);
            }
        }

        public static async Task BroadcastToAll(this List<DiscordChannel> discordChannels, string message)
        {
            if (discordChannels == null || discordChannels.Count <= 0) return;

            foreach (var channelToBroadcast in discordChannels)
            {
                await channelToBroadcast.SendMessageAsync(message);
            }
        }

        public static Dictionary<ulong, BroadcastData> CreateDummyBroadcasterList(int dummyCount = 10)
        {
            var dataList = new Dictionary<ulong, BroadcastData>();
            for (int i = 0; i < dummyCount; i++)
            {
                var data = GetRandomDummyBroadcastData(GetRandomDummyChannelID());
                dataList.Add(data.ChannelID, data);
            }

            DataGrabber.CreateConfig(JsonConvert.SerializeObject(dataList, Formatting.Indented));

            return dataList;
        }

        public static ulong GetRandomDummyChannelID() => (ulong)Random.Shared.NextInt64(1000000000000, 99999999999999);

        public static BroadcastData GetRandomDummyBroadcastData() => GetRandomDummyBroadcastData(GetRandomDummyChannelID());

        public static BroadcastData GetRandomDummyBroadcastData(ulong channelID)
        {
            return new BroadcastData()
            {
                ChannelID = channelID,
                PrefixesToTrack = new string[1] { GetRandomDummyGitPrefix() }
            };
        }

        public static string GetRandomDummyGitPrefix()
        {
            var randomList = new string[6]
            {
                "feature",
                "bug",
                "fix",
                "experimental",
                "demo",
                "hotfix"
            };

            return randomList[Random.Shared.Next(0, randomList.Length)];
        }
    }
}
