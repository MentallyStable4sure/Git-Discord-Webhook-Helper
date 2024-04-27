using DSharpPlus.Entities;

namespace MentallyStable.GitHelper.Services.Discord
{
    public interface IThreadWatcher
    {
        public Task CreateThread(DiscordChannel discordChannel, string title, DiscordMessageBuilder discordMessageBuilder);
        public Task CreateThread(List<DiscordChannel> discordChannels, string title, DiscordMessageBuilder discordMessageBuilder);

        public bool IsThreadCreated(DiscordChannel discordChannel, string[] lookupKeys);
        public bool IsThreadCreated(List<DiscordChannel> discordChannels, string[] lookupKeys);

        public DiscordThreadChannel FindThread(DiscordChannel channel, string[] lookupKeys);

        public Task Post(DiscordThreadChannel threadChannel, DiscordMessageBuilder threadedMessage);
    }
}
