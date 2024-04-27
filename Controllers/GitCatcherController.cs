using Newtonsoft.Json;
using DSharpPlus.Entities;
using Microsoft.AspNetCore.Mvc;
using MentallyStable.GitHelper.Data.Git;
using MentallyStable.GitHelper.Data.Database;
using MentallyStable.GitHelper.Data.Git.Gitlab;
using MentallyStable.GitHelper.Services.Discord;
using MentallyStable.GitHelper.Data.Development;
using MentallyStable.GitHelper.Services.Development;
using MentallyStable.GitHelper.Helpers;

namespace MentallyStable.GitHelper.Controllers
{
    [ApiController]
    [Route("git-catcher")]
    public class GitCatcherController : ControllerBase
    {
        private readonly IDebugger _debugger;
        private readonly BroadcastDataService _broadcastService;
        private readonly DiscordConfig _discordConfig;

        public GitCatcherController(IDebugger logger,
            BroadcastDataService broadcastService, DiscordConfig config) : base()
        {
            _debugger = logger;
            _broadcastService = broadcastService;
            _discordConfig = config;
        }

        [HttpPost("ping")]
        public string Ping() => "monke flip";

        [HttpPost("webhook-raw")]
        public async Task<string> Catch([FromBody] object body)
        {
            var response = JsonConvert.DeserializeObject<GitlabResponse>(body.ToString());
            response.ActionEventType = response.EventType.ToGitAction();

            _debugger.Log(response.ActionEventType.ToString(), new DebugOptions(this, "[webhook-raw]"));

            var message = PrettyViewService.WrapResponseInEmbed(response);
            await CatchAll(message);

            return "Data sent to a discord model successfully";
            //TODO: parse rawJson and check name of commit/pr, set it to prefix
            string[] prefixes = new string[0];
            //TODO: also set git actions according to what request it is, PR/Comment/Push/Merge/Close/etc.
            GitActionType[] actions = new GitActionType[0];

            //TODO: finally get channels to where we can post it
            //var channels = _broadcastService.GetChannelsByTypeAndPrefix();
            //_broadcastService.PostToChannels(channels);

            //return Ok($"Data sent to a discord model:\n {rawJson}");
        }

        private async Task CatchAll(string response)
        {
            if (_discordConfig.CatchAllAPI_ID <= 0) return;

            await _broadcastService.BroadcastMessageTo(_discordConfig.CatchAllAPI_ID, response);
        }

        private async Task CatchAll(DiscordMessageBuilder message)
        {
            if (_discordConfig.CatchAllAPI_ID <= 0) return;

            await _broadcastService.BroadcastMessageTo(_discordConfig.CatchAllAPI_ID, message);
        }
    }
}