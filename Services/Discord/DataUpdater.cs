using Newtonsoft.Json;
using MentallyStable.GitHelper.Data;
using MentallyStable.GitHelper.Data.Discord;
using MentallyStable.GitHelper.Data.Database;

namespace MentallyStable.GitHelper.Services.Discord
{
    public class DataUpdater
    {
        public static void UpdateBroadcastData(Dictionary<ulong, BroadcastData> broadcastData)
        {
            DataGrabber.CreateConfig(JsonConvert.SerializeObject(broadcastData), Endpoints.DISCORD_BROADCASTERS_CONFIG);
        }

        public static void UpdateEstablishedConnections(List<GitToDiscordLinkData> connections)
        {
            DataGrabber.CreateConfig(JsonConvert.SerializeObject(connections), Endpoints.ESTABLISHED_CONNECTIONS_CONFIG);
        }
    }
}
