using Newtonsoft.Json;
using DSharpPlus.Entities;
using Microsoft.AspNetCore.Mvc;
using MentallyStable.GitHelper.Helpers;
using MentallyStable.GitHelper.Data.Database;
using MentallyStable.GitHelper.Data.Git.Gitlab;
using MentallyStable.GitHelper.Services.Parsers;
using MentallyStable.GitHelper.Data.Development;
using MentallyStable.GitHelper.Services.Discord;
using MentallyStable.GitHelper.Services.Development;

namespace MentallyStable.GitHelper.Controllers
{
    [ApiController]
    [Route("git-catcher")]
    public class GitCatcherController : ControllerBase
    {
        private readonly IDebugger _debugger;
        private readonly DiscordConfig _discordConfig;
        private readonly IResponseParser<GitlabResponse> _gitResponseParser;
        private readonly GitCatcherHelper _catcherHelper;
        private readonly PrettyViewWrapService _prettyViewWrapService;
        private readonly BroadcastDataService _broadcastService;

        public GitCatcherController(IDebugger logger,
            DiscordConfig config,
            IResponseParser<GitlabResponse> responseParser,
            GitCatcherHelper catcherHelper,
            BroadcastDataService broadcasterService,
            PrettyViewWrapService prettyViewWrapService) : base()
        {
            _debugger = logger;
            _discordConfig = config;
            _gitResponseParser = responseParser;
            _catcherHelper = catcherHelper;
            _prettyViewWrapService = prettyViewWrapService;
            _broadcastService = broadcasterService;
        }

        [HttpPost("ping")]
        public string Ping() => "monke flip";

        [HttpPost("webhook-raw")]
        public async Task<string> Catch([FromBody] object body)
        {
            var response = JsonConvert.DeserializeObject<GitlabResponse>(body.ToString());
            Console.WriteLine(body);

            //parse action type if possible (if not parse prefixes) and if its not a comment create a new thread
            _debugger.Log(response.ObjectKind, new DebugOptions(this, "[webhook-raw]"));
            string[] lookupKeys = response.ObjectKind.ToLookupKeys(response);
            string[] loweredLookupKeys = response.ObjectKind.ToLookupKeysLowered(response);
            string[] identifiers = response.CreateIdentifiers();

            //catch all implementation if we've set a channel id (CatchAllAPI_ID) in discordconfig
            await CatchAll(await _prettyViewWrapService.WrapResponseInEmbed(response, response.ObjectKind, lookupKeys));
            var allPrefixes = _broadcastService.GetAllPrefixes();

            string title = lookupKeys.ToTitle();
            var threadedMessage = await _prettyViewWrapService.WrapResponseInEmbed(response, response.ObjectKind, lookupKeys);

            if (allPrefixes.Contains("all"))
            {
                await _catcherHelper.ForceCreateThreads(title, threadedMessage, identifiers, loweredLookupKeys);
            }
            else
            {
                //parse all out prefixes and see if it even needed to be tracked
                var prefixesFound = _gitResponseParser.ParsePrefixes(response, _broadcastService.GetAllPrefixes());
                if (prefixesFound.Length <= 0) return $"<h4>We have not found any prefixes tracked in your response, if this problem persist check if you have any prefixes you track in configs/{Endpoints.DISCORD_BROADCASTERS_CONFIG}</h4>";

                var channelsTracked = _broadcastService.GetChannels(prefixesFound);

                await _catcherHelper.ParsePrefixesAndCreateThread(channelsTracked, loweredLookupKeys, 
                    prefixesFound, title, threadedMessage, identifiers, response);
            }

            return "Data sent to a discord model successfully";
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