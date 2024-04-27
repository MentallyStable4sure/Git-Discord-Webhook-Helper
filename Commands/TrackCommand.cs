using DSharpPlus.SlashCommands;
using MentallyStable.GitHelper.Services.Discord;

namespace MentallyStable.GitHelper.Commands
{
    public class TrackCommand : ApplicationCommandModule
    {
        public BroadcastDataService BroadcastDataService { get; set; }
        public TrackingService TrackingService { get; set; }

        [SlashCommand("track", "Gives all the prefixes presets avaliable ATM (we can add issues, wiki, pipelines, etc. in the future)")]
        public async Task TrackChannel(InteractionContext ctx, [Option("Prefixes", "prefixes to keep track by this channel (leave empty to track everything [NOT RECOMMENDED])")] string[] prefixes = null)
        {
            if(prefixes == null || prefixes.Length == 0) prefixes = new string[1] { "all" };
            TrackingService.TrackChannel(ctx.Channel, prefixes);

            string allPrefxiesCollected = "> ";
            foreach (var prefix in prefixes)
            {
                allPrefxiesCollected += $"{prefix}; ";
            }

            await ctx.CreateResponseAsync($"> Successfully added channel {ctx.Channel.Mention} to tracked channels with prefixes: \n{allPrefxiesCollected}");
        }

        [SlashCommand("untrack", "removes this channel (on what channel you type right now) from a tracking ones by git webhooks (with all prefixes)")]
        public async Task UntrackChannel(InteractionContext ctx)
        {
            var data = BroadcastDataService.GetChannelData(ctx.Channel.Id);

            string message = string.Empty;
            if (data == null) message = $"> Channel {ctx.Channel.Mention} wasnt tracked before, not found any prefixes attached to this channel";
            else message = $"> Successfully removed channel {ctx.Channel.Mention} from tracked channels";

            TrackingService.UntrackChannel(ctx.Channel);
            await ctx.CreateResponseAsync(message);
        }
    }
}
