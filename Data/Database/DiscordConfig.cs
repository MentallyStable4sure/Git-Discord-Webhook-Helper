using DSharpPlus;

namespace MentallyStable.GitlabHelper.Data.Database
{
    public class DiscordConfig
    {
        public string Token = "your_token_from_discord_devloper";
        public int Type = (int)TokenType.Bot;
        public bool AutoReconnect = true;

        public ulong CustomGuidID = 0; //custom server with instant refresh (for testing)
    }
}
