using Microsoft.AspNetCore.Mvc;
using MentallyStable.GitHelper.Data.Git;
using MentallyStable.GitHelper.Services.Discord;
using MentallyStable.GitHelper.Data.Database;

namespace MentallyStable.GitHelper.Controllers
{
    [ApiController]
    [Route("git-catcher")]
    public class GitCatcherController : ControllerBase
    {
        private readonly ILogger<GitCatcherController> _logger;
        private readonly BroadcastDataService _broadcastService;
        private readonly DiscordConfig _discordConfig;

        public GitCatcherController(ILogger<GitCatcherController> logger,
            BroadcastDataService broadcastService, DiscordConfig config) : base()
        {
            _logger = logger;
            _broadcastService = broadcastService;
            _discordConfig = config;
        }

        [HttpPost("ping")]
        public string Ping() => "monke flip";

        [HttpPost("webhook-raw")]
        public async Task<string> Catch([FromBody] string rawJson)
        {
            await CatchAll(rawJson);
            return $"Data sent to a discord model:\n {rawJson}";
            //TODO: parse rawJson and check name of commit/pr, set it to prefix
            string[] prefixes = new string[0];
            //TODO: also set git actions according to what request it is, PR/Comment/Push/Merge/Close/etc.
            GitActionType[] actions = new GitActionType[0];

            //TODO: finally get channels to where we can post it
            //var channels = _broadcastService.GetChannelsByTypeAndPrefix();
            //_broadcastService.PostToChannels(channels);

            //return Ok($"Data sent to a discord model:\n {rawJson}");
        }

        [HttpPost("webhook-raw")]
        public async Task<string> Catch([FromBody] JsonContent rawJson) => await Catch(rawJson.ToString());

        private async Task CatchAll(string response)
        {
            if (_discordConfig.CatchAllAPI_ID <= 0) return;

            await _broadcastService.BroadcastMessageTo(_discordConfig.CatchAllAPI_ID, response);
        }
    }
}