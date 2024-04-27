using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using MentallyStable.GitHelper.Extensions;
using MentallyStable.GitHelper.Services.Discord;

namespace MentallyStable.GitHelper.Commands
{
    public class InfoCommand : ApplicationCommandModule
    {
        public BroadcastDataService BroadcastDataService { get; set; }

        [SlashCommand("allprefixes", "Gives all the prefixes presets avaliable ATM (we can add issues, wiki, pipelines, etc. in the future)")]
        public async Task AllPrefixes(InteractionContext ctx)
        {
            string prefixesInfo = "> merge_request - shows all merge requests\n> note - shows all comments\n> opened - shows only opening merge requests or comments to opened prs (if goes with note)\n> closed - shows only closing merge requests or comments to closed prs (if goes with note)\n> push - shows all the pushes (like, literally, every commit/pr/issue push, be careful)\n> commit - shows pushes for commits only (still a lot)\n> all - track everything (not recommended unless u r testing or a stalker)\n> ANY PREFIX U WANT! - u can add something like 'feature', 'fix', etc. (lowercase) and it will track just fine";
            var builder = new DiscordInteractionResponseBuilder()
                .GetInfoEmbed( "PREFIXES AVALIABLE:", prefixesInfo);

            await ctx.CreateResponseAsync(builder);
        }

        [SlashCommand("current-channel-prefixes", "Gives you this channel tracked prefixes (if set)")]
        public async Task CurrentPrefixes(InteractionContext ctx)
        {
            var data = BroadcastDataService.GetChannelData(ctx.Channel.Id);
            if (data == null)
            {
                await ctx.CreateResponseAsync("> No prefixes found. Use /track [PREFIX, PREFIXm, PREFIX,...] to track prefixes on this channel");
                return;
            }

            string allPrefxiesCollected = "> ";
            foreach (var prefix in data.PrefixesToTrack)
            {
                allPrefxiesCollected += $"{prefix}; ";
            }

            await ctx.CreateResponseAsync(allPrefxiesCollected);
        }
    }
}
