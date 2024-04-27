﻿using DSharpPlus;
using DSharpPlus.Entities;
using MentallyStable.GitHelper.Helpers;
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

        public DiscordChannel GetChannel(string channelID) => _broadcastData[ulong.Parse(channelID)]?.DiscodChannelReference;
        public DiscordChannel GetChannel(ulong channelID) => _broadcastData[channelID]?.DiscodChannelReference;
        public DiscordChannel GetChannel(BroadcastData data) => GetChannel(data.ChannelID);

        public List<DiscordChannel> GetChannels(string[] prefixes)
        {
            var channels = new List<DiscordChannel>();
            foreach (var data in _broadcastData.Values)
            {
                foreach (var prefixInData in data.PrefixesToTrack)
                {
                    if (!prefixes.Any(element => element == prefixInData)) continue;

                    var duplicate = channels.FirstOrDefault(channelToSeek => data.ChannelID == channelToSeek.Id);
                    if (duplicate != null) continue;

                    channels.Add(data.DiscodChannelReference);
                }
            }

            return channels;
        }

        public string[] GetAllPrefixes()
        {
            List<string> prefixesCollected = new List<string>();

            foreach (var data in _broadcastData.Values)
            {
                foreach (var prefix in data.PrefixesToTrack)
                {
                    if(prefixesCollected.Contains(prefix)) continue;
                    prefixesCollected.Add(prefix);
                }
            }

            return prefixesCollected.ToArray();
        }

        public async Task BroadcastMessageAccordingToPrefix(string[] prefixes, string message) => await GetChannels(prefixes).BroadcastToAll(message);
        public async Task BroadcastMessageAccordingToPrefix(string[] prefixes, DiscordMessageBuilder message) => await GetChannels(prefixes).BroadcastToAll(message);

        public async Task BroadcastMessageTo(ulong channelID, string message)
        {
            var channel = await _client.GetChannelAsync(channelID);
            await channel.SendMessageAsync(message);
        }

        public async Task BroadcastMessageTo(ulong channelID, DiscordMessageBuilder message)
        {
            var channel = await _client.GetChannelAsync(channelID);
            await channel.SendMessageAsync(message);
        }

        private async Task CacheChannelsAsync()
        {
            if (_broadcastData == null || _broadcastData.Count <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                _debugger.Log("NO CHANNELS TO KEEP TRACK OF FOUND! EDIT YOUR configs/discordbroadcasters.json", new DebugOptions());
                Console.ResetColor();
                return;
            }

            foreach (var dataItem in _broadcastData.Values)
            {
                dataItem.DiscodChannelReference = await _client.GetChannelAsync(dataItem.ChannelID);
            }
        }
    }
}
