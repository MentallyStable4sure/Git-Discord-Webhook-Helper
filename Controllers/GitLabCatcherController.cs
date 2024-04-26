using Microsoft.AspNetCore.Mvc;
using MentallyStable.GitHelper.Data.Git;
using MentallyStable.GitHelper.Services.Discord;

namespace MentallyStable.GitHelper.Controllers
{
    [ApiController]
    [Route("gitlab-catcher")]
    public class GitLabCatcherController : ControllerBase
    {
        private readonly ILogger<GitLabCatcherController> _logger;
        private readonly BroadcastDataService _broadcastService;

        public GitLabCatcherController(ILogger<GitLabCatcherController> logger,
            BroadcastDataService broadcastService)
        {
            _logger = logger;
            _broadcastService = broadcastService;
        }

        [HttpPost("webhook-raw")]
        public async Task<IActionResult> Catch(string rawJson)
        {
            //TODO: parse rawJson and check name of commit/pr, set it to prefix
            string[] prefixes = new string[0];
            //TODO: also set git actions according to what request it is, PR/Comment/Push/Merge/Close/etc.
            GitActionType[] actions = new GitActionType[0];

            //TODO: finally get channels to where we can post it
            //var channels = _broadcastService.GetChannelsByTypeAndPrefix();
            //_broadcastService.PostToChannels(channels);

            return Ok($"Data sent to a discord model:\n {rawJson}");
        }
    }
}