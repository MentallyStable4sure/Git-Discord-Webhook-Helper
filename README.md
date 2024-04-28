# Git Discord Helper for teams

> No matter what git system u use as long as you have pull requests/comments/commits u can use this system. Everything is changable thru /configs folder. It contains <a href="https://github.com/MentallyStable4sure/Git-Discord-Webhook-Helper/blob/main/configs/discordbroadcasters.json">discordbroadcasters.json</a> which can be changed to your channels/actions to track. To start bot simply change <a href="https://github.com/MentallyStable4sure/Git-Discord-Webhook-Helper/blob/main/configs/discordconfig.json">discordconfig.json</a> with your token n stuff and boot up.

<br>

  <details>
  <summary>✏️ COMMANDS AVALIABLE ATM: ✏️</summary>

  ```
  /help - all commands list.
  /allprefixes - all possible pre-generated prefixes (you can, and i suggest to add yours too)

  /news [TITLE] [DESCRIPTION] [URL (optional)] - Posts an embedded with title, description, author.
  /news-short [DESCRIPTION] - Posts short version of embedded as '/news' but no author and title.

  /track [PREFIX] [PREFIX2 (optional)] - Starts to track channel for given prefixes.
  /untrack - Removes tracking and deletes all prefixes from current channel.

  /addprefix [PREFIX] - adds a prefix to track in current channel.
  /removeprefix [PREFIX] - removes prefix from tracking in current channel.

  /current-channel-prefixes - see the list of prefixes that this channel tracks.

  /link [GIT_USERNAME] - linking your profile (will work for @ mentions and thumbnails)
  /unlink [GIT_USERNAME] - will unlink identifier from all users/channels
  /connections - check all your connections linked (bot will DM identifiers)
  ```
  </details>

  
<hr>

  <details>
  <summary>💬 I MIGHT ADD: 💬</summary>

  ```
  /pipeline [pipeline_id] - to add your build pipelines to track
  /build [pipeline_id] - to execute building pipeline from discord


  /addprefix [wiki_page, issues, pipeline] - more predifined settings

  /is-able-to-merge - type in thread to see if PR has conflicts
  ```
  </details>

<hr>

<br></br>

  ## 🔗🎲 How to setup?:

  ### Host it yourself on webapp and test in GitLab:
  > Download latest <a href="https://github.com/MentallyStable4sure/Git-Discord-Webhook-Helper/releases">release</a>, unpack, it, launch .exe, or if u r using ubuntu/linux/apache web server/etc. then upload all files on the server, change config/discordconfig.json to your needs (token from discord developer portal and CatchAllAPI_ID of your discord text channel to receive things), then use command on your server:

  ```
  cd /your_folder_with_this_project - to navigate to the folder
  dotnet GitDiscordWebhookHelper.dll - to execute .NET application
  ```
  > Now you can can just knock to:
  ```
  http://localhost:8801/git-catcher/webhook-raw - if you skipped hosting on web server
  http://{your-web-ip}:8801/git-catcher/webhook-raw - paste it to the GitLab Webhook in the settings
  ```
  > Instead of localhost paste your server IP if its hosted on the server, then copy this entire address and paste into Gitlab > Settings > Webhooks (in your project, its the left menu bar at the bottom). Then paste your url into webhook field, mark what events you want to receive and press Add Webhook at the bottom. Thats it, you can test it at the bottom from dropdown menu. Have fun!
  
  <p align=center>
    <img src="https://github.com/MentallyStable4sure/Git-Discord-Webhook-Helper/assets/62771181/2e34804e-3a9e-4c63-b04a-61c441192df4" align=center width=300 height=300>
    <img src="https://github.com/MentallyStable4sure/Git-Discord-Webhook-Helper/assets/62771181/7958aa3d-fa18-486d-adfa-16118fe849d2" align=center width=350 height=300>
  </p>



  > You can easily test if API receiveing requests by posting their examples from webhook page or accessing ping endpoint:
  ```
  http://localhost:8801/git-catcher/ping
  ```

<br>

### Fast way. Launch and test (you can do postman or anything like that):
> - Clone repository
> - Change config/discordconfig to setup your token from discord developer portal
> - Open it in visual studio, run
> - Knock on localhost:

  ```
  http://localhost:8801/git-catcher/webhook-raw
  ```
  
<br></br>

> Powered on .ASP Web API [.NET 6]
