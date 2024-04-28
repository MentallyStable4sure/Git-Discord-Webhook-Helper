using DSharpPlus.SlashCommands;
using MentallyStable.GitHelper.Services.Discord;

namespace MentallyStable.GitHelper.Commands
{
    public class TrackCommand : ApplicationCommandModule
    {
        public BroadcastDataService BroadcastDataService { get; set; }
        public TrackingService TrackingService { get; set; }

        [SlashCommand("track", "adds this channel (which u typing in rn) to a tracking ones with prefixes provided")]
        public async Task TrackChannel(InteractionContext ctx, [Option("Prefixes", "prefixes to keep track by this channel ('all' if null [NOT RECOMMENDED])")] string prefix1, [Option("Prefix2", "More prefixes to track (optional)")] string prefix2 = null, [Option("Prefix3", "More prefixes to track (optional)")]  string prefix3 = null)
        {
            if (TrackingService.IsChannelTracked(ctx.Channel.Id))
            {
                await ctx.CreateResponseAsync($"> ❌ ** {ctx.Channel.Mention} ** is __already tracked__, to add more prefixes use **/addprefix** 🚩");
                return;
            }

            List<string> prefixes = new List<string> { prefix1?.ToLower(), prefix2?.ToLower(), prefix3?.ToLower() };
            List<string> actualPrefixes = new List<string>();
            foreach (var prefix in prefixes)
            {
                if (prefix == null || prefix.Length <= 0) continue;
                actualPrefixes.Add(prefix);
            }

            TrackingService.TrackChannel(ctx.Channel, actualPrefixes.ToArray());

            string allPrefxiesCollected = "> ";
            foreach (var prefix in actualPrefixes)
            {
                allPrefxiesCollected += $"{prefix}; ";
            }

            await ctx.CreateResponseAsync($"> ✅ Successfully added channel ** {ctx.Channel.Mention} ** to tracked channels with prefixes: \n{allPrefxiesCollected}");
        }

        [SlashCommand("untrack", "removes this channel (which u typing in rn) from a tracking ones by git webhooks (with all prefixes)")]
        public async Task UntrackChannel(InteractionContext ctx)
        {
            if (!TrackingService.IsChannelTracked(ctx.Channel.Id))
            {
                await ctx.CreateResponseAsync($"> ❌ ** {ctx.Channel.Mention} ** was not tracked before, no changes made");
                return;
            }

            var data = BroadcastDataService.GetChannelData(ctx.Channel.Id);

            string message = string.Empty;
            if (data == null) message = $"> ❌ Channel ** {ctx.Channel.Mention} ** wasnt tracked before, __not found__ any prefixes attached to this channel";
            else message = $"> ✅ Successfully removed channel ** {ctx.Channel.Mention} ** from tracked channels";

            TrackingService.UntrackChannel(ctx.Channel);
            await ctx.CreateResponseAsync(message);
        }
    }
}
