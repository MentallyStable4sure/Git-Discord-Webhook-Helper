using DSharpPlus;

namespace MentallyStable.GitHelper.Data.Database
{
    public class DiscordConfig
    {
        public string Token = "your_token_from_discord_devloper";
        public int Type = (int)TokenType.Bot;
        public bool AutoReconnect = true;

        public ulong CustomGuidID = 0; //custom server with instant refresh (for testing)
        public ulong CatchAllAPI_ID = 0; //custom catch all developer channel id (for testing)
    }
}
