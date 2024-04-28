using DSharpPlus;
using DSharpPlus.Entities;

namespace MentallyStable.GitHelper.Services.Discord
{
    public class ThreadWatcherService : IService, IThreadWatcher
    {
        private readonly UserLinkEstablisherService _userLinkEstablisherService;
        private readonly DiscordClient _client;

        public ThreadWatcherService(UserLinkEstablisherService userLinkEstablisherService,
            DiscordClient client)
        {
            _userLinkEstablisherService = userLinkEstablisherService;
            _client = client;
        }

        public Task InitializeService() => Task.CompletedTask;

        public async Task CreateThread(DiscordChannel discordChannel, string title, DiscordMessageBuilder discordMessageBuilder, string[] identifiers)
        {
            var message = await discordMessageBuilder.SendAsync(discordChannel);
            var thread = await discordChannel.CreateThreadAsync(message, title, AutoArchiveDuration.ThreeDays);
            await AddNeededMembersToThread(thread, identifiers);
        }

        public async Task AddNeededMembersToThread(DiscordThreadChannel thread, string[] identifiers)
        {
            var links = _userLinkEstablisherService.GetConnections(identifiers);
            foreach (var link in links)
            {
                var user = await _client.GetUserAsync(link.DiscordSnowflakeId);
                var message = await thread.SendMessageAsync($"@silent {user.Mention}");
                await message.ModifyAsync($"🎲 You've been auto-invited by identifier parsing: {link.GitUniqueIdentifier} ✨");
            }
        }

        public async Task CreateThread(List<DiscordChannel> discordChannels, string title, DiscordMessageBuilder discordMessageBuilder, string[] identifiers)
        {
            foreach (var channel in discordChannels)
            {
                var message = await discordMessageBuilder.SendAsync(channel);
                var thread = await channel.CreateThreadAsync(message, title, AutoArchiveDuration.ThreeDays);
                await AddNeededMembersToThread(thread, identifiers);
            }
        }

        public bool IsThreadCreated(DiscordChannel discordChannel, string[] lookupKeys)
        {
            if (discordChannel.Threads == null || discordChannel.Threads.Count <= 0) return false;
            foreach (var thread in discordChannel.Threads)
            {
                if (lookupKeys.Contains($"WIP: {thread.Name}".ToLower()) || lookupKeys.Contains(thread.Name.ToLower())) return true;
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
                if (lookupKeys.Contains(thread.Name.ToLower()) || lookupKeys.Contains($"WIP: {thread.Name}".ToLower())) return thread;
            }

            return null;
        }

        public async Task Post(DiscordThreadChannel threadChannel, DiscordMessageBuilder threadedMessage) => await threadChannel.SendMessageAsync(threadedMessage);

        public async Task RemoveEveryone(DiscordThreadChannel threadChannel)
        {
            var members = await threadChannel.ListJoinedMembersAsync();
            foreach (var member in members)
            {
                await threadChannel.RemoveThreadMemberAsync(member.Member);
            }
        }
    }
}
