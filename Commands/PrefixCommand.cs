using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using MentallyStable.GitHelper.Services.Discord;

namespace MentallyStable.GitHelper.Commands
{
    public class PrefixCommand : ApplicationCommandModule
    {
        public BroadcastDataService BroadcastDataService { get; set; }
        public TrackingService TrackingService { get; set; }

        [SlashCommand("addprefix", "adds prefix to track on this channel (creates a tracking if channel wasnt tracked)")]
        public async Task AddPrefix(InteractionContext ctx, [Option("Prefix", "Additional prefix (if this channel has any, if not - creates track step) to track on this channel")] string prefix)
        {
            string message = string.Empty;
            DiscordChannel channel = ctx.Channel;
            prefix = prefix.ToLower();

            var data = BroadcastDataService.GetChannelData(channel.Id);
            if (TrackingService.IsChannelTracked(channel.Id))
            {
                TrackingService.AddPrefix(channel, prefix);
                message = $"> ✅ Successfully added prefix ** '{prefix}' ** to a ** {channel.Mention} ** to keep track of ✨";
            }
            else
            {
                TrackingService.TrackChannel(channel, new string[1] { prefix });
                message = $"> ❌ Channel {channel.Mention} wasnt tracked __before__, so we __added__ it to a tracked list with prefix ** '{prefix}' ** ";
            }

            await ctx.CreateResponseAsync(message);
        }

        [SlashCommand("removeprefix", "adds prefix to track on this channel (creates a tracking if channel wasnt tracked)")]
        public async Task RemovePrefix(InteractionContext ctx, [Option("Prefix", "Additional prefix (if this channel has any, if not - creates track step) to track on this channel")] string prefix)
        {
            string message = string.Empty;
            DiscordChannel channel = ctx.Channel;
            prefix = prefix.ToLower();

            var data = BroadcastDataService.GetChannelData(channel.Id);
            if (TrackingService.IsChannelTracked(channel.Id))
            {
                TrackingService.RemovePrefix(channel, prefix);
                message = $"> ✅ Successfully removed prefix ** '{prefix}' ** to a ** {channel.Mention} ** to keep track of ✨";

                if (!TrackingService.IsChannelTracked(channel.Id)) message = $"> ✅ Removed prefix ** '{prefix}' ** from ** {channel.Mention}, ** but it looks like it was the last prefix, so channel was __deleted__ from tracked list";
            }
            else
            {
                message = $"> ❌ Channel {channel.Mention} wasnt tracked before by any prefixes, so there is no need to untrack any prefix.";
            }

            await ctx.CreateResponseAsync(message);
        }
    }
}
