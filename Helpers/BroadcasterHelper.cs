
using Newtonsoft.Json;
using MentallyStable.GitlabHelper.Data;
using MentallyStable.GitlabHelper.Data.Git;
using MentallyStable.GitlabHelper.Data.Discord;

namespace MentallyStable.GitlabHelper.Helpers
{
    public class BroadcasterHelper
    {
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

        public static GitActionType GetRandomDummyGitActionType() => (GitActionType)Random.Shared.Next(0, Enum.GetValues(typeof(GitActionType)).Length);

        public static BroadcastData GetRandomDummyBroadcastData() => GetRandomDummyBroadcastData(GetRandomDummyChannelID());

        public static BroadcastData GetRandomDummyBroadcastData(ulong channelID)
        {
            return new BroadcastData()
            {
                ChannelID = channelID,
                ActionsToTrack = new GitActionType[1] { GetRandomDummyGitActionType() },
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
