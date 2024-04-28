using DSharpPlus;
using DSharpPlus.Entities;
using MentallyStable.GitHelper.Data.Discord;

namespace MentallyStable.GitHelper.Services.Discord
{
    public class TrackingService : IService
    {
        private readonly DiscordClient _client;
        
        private readonly Dictionary<ulong, BroadcastData> _broadcastData;

        public TrackingService(DiscordClient client, Dictionary<ulong, BroadcastData> broadcastData)
        {
            _client = client;
            _broadcastData = broadcastData;
        }

        public Task InitializeService() => Task.CompletedTask;

        /// <summary>
        /// NOT Recommended due to double async getter of a channel from client, if you already have a channel reference
        /// <br>BETTER to use it in <see cref="TrackChannel(DiscordChannel, string[])"/></br>
        /// </summary>
        /// <param name="channelId">channel id to track</param>
        /// <param name="prefixes">prefixes to track in this channel</param>
        public async Task TrackChannel(ulong channelId, string[] prefixes)
        {
            var channel = await _client.GetChannelAsync(channelId);
            if (_broadcastData.ContainsKey(channelId))
            {
                if (_broadcastData[channelId].PrefixesToTrack == prefixes) return;
                _broadcastData[channelId].PrefixesToTrack = prefixes;
                _broadcastData[channelId].DiscodChannelReference = channel;
                DataUpdater.UpdateBroadcastData(_broadcastData);
                return;
            }

            _broadcastData.Add(channelId, new BroadcastData()
            {
                ChannelID = channelId,
                PrefixesToTrack = prefixes,
                DiscodChannelReference = channel
            });
            DataUpdater.UpdateBroadcastData(_broadcastData);
        }

        public void TrackChannel(DiscordChannel channel, string[] prefixes)
        {
            if (_broadcastData.ContainsKey(channel.Id))
            {
                if (_broadcastData[channel.Id].PrefixesToTrack == prefixes) return;
                _broadcastData[channel.Id].PrefixesToTrack = prefixes;
                _broadcastData[channel.Id].DiscodChannelReference = channel;
                DataUpdater.UpdateBroadcastData(_broadcastData);
                return;
            }

            _broadcastData.Add(channel.Id, new BroadcastData()
            {
                ChannelID = channel.Id,
                PrefixesToTrack = prefixes,
                DiscodChannelReference = channel
            });
            DataUpdater.UpdateBroadcastData(_broadcastData);
        }

        public void UntrackChannel(DiscordChannel channel)
        {
            if (!_broadcastData.ContainsKey(channel.Id)) return;
            _broadcastData.Remove(channel.Id);
            DataUpdater.UpdateBroadcastData(_broadcastData);
        }

        public void UntrackChannel(ulong channelId)
        {
            if (!_broadcastData.ContainsKey(channelId)) return;
            _broadcastData.Remove(channelId);
            DataUpdater.UpdateBroadcastData(_broadcastData);
        }

        public void AddPrefix(DiscordChannel channel, string prefix)
        {
            if (!IsChannelTracked(channel.Id)) return;
            if (_broadcastData[channel.Id].PrefixesToTrack.Contains(prefix)) return;

            var list = _broadcastData[channel.Id].PrefixesToTrack.ToList();
            list.Add(prefix);
            _broadcastData[channel.Id].PrefixesToTrack = list.ToArray();

            DataUpdater.UpdateBroadcastData(_broadcastData);
        }

        public void RemovePrefix(DiscordChannel channel, string prefix)
        {
            if (!IsChannelTracked(channel.Id)) return;
            if (!_broadcastData[channel.Id].PrefixesToTrack.Contains(prefix)) return;

            var list = _broadcastData[channel.Id].PrefixesToTrack.ToList();
            list.Remove(prefix);
            _broadcastData[channel.Id].PrefixesToTrack = list.ToArray();

            DataUpdater.UpdateBroadcastData(_broadcastData);
        }

        public bool IsChannelTracked(ulong channelId) => _broadcastData.ContainsKey(channelId);
    }
}
