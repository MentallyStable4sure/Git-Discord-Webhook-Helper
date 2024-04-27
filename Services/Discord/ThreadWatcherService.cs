using DSharpPlus;
using DSharpPlus.Entities;

namespace MentallyStable.GitHelper.Services.Discord
{
    public class ThreadWatcherService : IService, IThreadWatcher
    {
        public Task InitializeService() => Task.CompletedTask;

        public async Task CreateThread(DiscordChannel discordChannel, string title, DiscordMessageBuilder discordMessageBuilder)
        {
            var message = await discordMessageBuilder.SendAsync(discordChannel);
            await discordChannel.CreateThreadAsync(message, title, AutoArchiveDuration.ThreeDays);
        }

        public async Task CreateThread(List<DiscordChannel> discordChannels, string title, DiscordMessageBuilder discordMessageBuilder)
        {
            foreach (var channel in discordChannels)
            {
                var message = await discordMessageBuilder.SendAsync(channel);
                await channel.CreateThreadAsync(message, title, AutoArchiveDuration.ThreeDays);
            }
        }

        public bool IsThreadCreated(DiscordChannel discordChannel, string[] lookupKeys)
        {
            foreach (var thread in discordChannel.Threads)
            {
                if (!lookupKeys.Contains(thread.Name)) continue;
                return true;
            }

            return false;
        }

        public bool IsThreadCreated(List<DiscordChannel> discordChannels, string[] lookupKeys)
        {
            foreach (var channel in discordChannels)
            {
                if(IsThreadCreated(channel, lookupKeys)) return true;
            }

            return false;
        }

        public DiscordThreadChannel FindThread(DiscordChannel channel, string[] lookupKeys)
        {
            foreach (var thread in channel.Threads)
            {
                if (lookupKeys.Contains(thread.Name)) return thread;
            }

            return null;
        }

        public async Task Post(DiscordThreadChannel threadChannel, DiscordMessageBuilder threadedMessage) => await threadChannel.SendMessageAsync(threadedMessage);
    }
}
