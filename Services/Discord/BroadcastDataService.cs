using DSharpPlus;
using DSharpPlus.Entities;
using MentallyStable.GitHelper.Data.Discord;
using MentallyStable.GitHelper.Registrators;
using MentallyStable.GitHelper.Data.Development;
using MentallyStable.GitHelper.Services.Development;

namespace MentallyStable.GitHelper.Services.Discord
{
    public class BroadcastDataService : IService
    {
        private Dictionary<ulong, BroadcastData> _broadcastData = new Dictionary<ulong, BroadcastData>();

        private readonly DiscordClient _client;
        private readonly IDebugger _debugger = new Debugger();

        public BroadcastDataService(ConfigsRegistrator configs, DiscordClient client)
        {
            _broadcastData = configs.BroadcastData;
            _client = client;
        }

        public async Task InitializeService()
        {
            _broadcastData ??= new Dictionary<ulong, BroadcastData>();

            await _debugger.TryExecuteAsync(CacheChannelsAsync(), new DebugOptions(this, nameof(CacheChannelsAsync)));
        }

        public DiscordChannel GetChannel(string channelID) => _broadcastData[ulong.Parse(channelID)].DiscodChannelReference;
        public DiscordChannel GetChannel(ulong channelID) => _broadcastData[channelID].DiscodChannelReference;
        public DiscordChannel GetChannel(BroadcastData data) => GetChannel(data.ChannelID);

        private async Task CacheChannelsAsync()
        {
            foreach (var dataItem in _broadcastData.Values)
            {
                dataItem.DiscodChannelReference = await _client.GetChannelAsync(dataItem.ChannelID);
            }
        }
    }
}
