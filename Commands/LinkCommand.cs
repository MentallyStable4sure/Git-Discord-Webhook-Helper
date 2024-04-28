using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using MentallyStable.GitHelper.Data.Discord;
using MentallyStable.GitHelper.Services.Discord;

namespace MentallyStable.GitHelper.Commands
{
    public class LinkCommand : ApplicationCommandModule
    {
        public BroadcastDataService BroadcastDataService { get; set; }
        public UserLinkEstablisherService LinkEstablisherService { get; set; }

        public const string DM_PRIVATE_GIF = "https://bunbun.cloud/admin/funkymonke/img/dm_orangutang.gif";

        [SlashCommand("link", "links your git username with your discord profile (for notification/avatar-thumbnails/etc.)")]
        public async Task LinkAccount(InteractionContext ctx, [Option("Identifier", "Supports: username (j.bieber), you can type email or name (Justin Bieber) but notify will not work")] string gitIdentifier)
        {
            if (LinkEstablisherService.GetConnection(ctx.User.Id) != null)
            {
                await ctx.CreateResponseAsync($"> Looks like you 🔗 **already linked** 🔗 your profile or this identifier, u can unlink by __/unlink {gitIdentifier}__. We will **🆕 re-link 🔗** your account now.");
            }

            string message = string.Empty;
            LinkEstablisherService.LinkAccount(gitIdentifier, ctx.User.Id);

            await ctx.CreateResponseAsync($"> ✅ Successfully linked {gitIdentifier} 🔗 {ctx.User.Mention}");
        }

        [SlashCommand("unlink", "removes this your gitIdentifier from all data channels ever tracked and deletes a connection")]
        public async Task UnlinkAccount(InteractionContext ctx, [Option("Identifier", "You need to specify your identifier exactly, if you dont remember use /connections")] string gitIdentifier)
        {
            if(LinkEstablisherService.GetConnection(ctx.User.Id) == null)
            {
                await ctx.CreateResponseAsync($"> ❌ No connections found by identifier {gitIdentifier}⛓️");
                return;
            }

            LinkEstablisherService.UnlinkAccount(gitIdentifier);
            await ctx.CreateResponseAsync($"> ✅ Link 🔗 successfully removed from {gitIdentifier}⛓️");
        }

        [SlashCommand("connections", "will send you all your connections to the account (don't worry, we will DM you in private)")]
        public async Task Connections(InteractionContext ctx)
        {
            GitToDiscordLinkData linkData = LinkEstablisherService.GetConnection(ctx.User.Id);

            var dm = await ctx.Member.CreateDmChannelAsync();
            await dm.SendMessageAsync(new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Red,
                ImageUrl = DM_PRIVATE_GIF,
                Description = $"> GitIdentifier: {linkData.GitUniqueIdentifier}🔗{linkData.DiscordSnowflakeId} :Discord ID"
            });
        }
    }
}
