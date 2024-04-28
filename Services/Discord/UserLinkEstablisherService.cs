using MentallyStable.GitHelper.Data.Discord;

namespace MentallyStable.GitHelper.Services.Discord
{
    public class UserLinkEstablisherService : IService
    {
        private readonly List<GitToDiscordLinkData> _establishedConnections;

        public UserLinkEstablisherService(List<GitToDiscordLinkData> linkDatas)
        {
            _establishedConnections = linkDatas;
        }

        public Task InitializeService() => Task.CompletedTask;

        public GitToDiscordLinkData GetConnection(ulong id)
        {
            foreach (var link in _establishedConnections)
            {
                if (string.IsNullOrEmpty(link.GitUniqueIdentifier)) continue;
                if (link.DiscordSnowflakeId != id) continue;
                return link;
            }

            return null;
        }

        public GitToDiscordLinkData GetConnection(string[] identifiers)
        {
            foreach (var link in _establishedConnections)
            {
                if (link.DiscordSnowflakeId <= 0) continue;
                if (!identifiers.Contains(link.GitUniqueIdentifier)) continue;
                return link;
            }

            return null;
        }

        public List<GitToDiscordLinkData> GetConnections(string[] identifiers)
        {
            var connections = new List<GitToDiscordLinkData>();
            foreach (var link in _establishedConnections)
            {
                if (link.DiscordSnowflakeId <= 0) continue;
                if (!identifiers.Contains(link.GitUniqueIdentifier)) continue;
                connections.Add(link);
            }

            return connections;
        }

        public GitToDiscordLinkData GetConnection(string identifier)
        {
            foreach (var link in _establishedConnections)
            {
                if (link.DiscordSnowflakeId <= 0) continue;
                if (!identifier.Contains(link.GitUniqueIdentifier)) continue;
                return link;
            }

            return null;
        }

        public GitToDiscordLinkData GetConnectionStrict(string identifier)
        {
            foreach (var link in _establishedConnections)
            {
                if (link.DiscordSnowflakeId <= 0) continue;
                if (identifier != link.GitUniqueIdentifier) continue;
                return link;
            }

            return null;
        }

        public void LinkAccount(string gitIdentifier, ulong userId)
        {
            var alreadyEstablishedLink = GetConnection(userId);

            if(alreadyEstablishedLink != null)
            {
                if (alreadyEstablishedLink.GitUniqueIdentifier == gitIdentifier 
                    && alreadyEstablishedLink.DiscordSnowflakeId == userId) return;

                alreadyEstablishedLink.GitUniqueIdentifier = gitIdentifier;
                alreadyEstablishedLink.DiscordSnowflakeId = userId;
                return;
            }

            var newLink = new GitToDiscordLinkData()
            {
                DiscordSnowflakeId = userId,
                GitUniqueIdentifier = gitIdentifier,
            };

            _establishedConnections.Add(newLink);
            DataUpdater.UpdateEstablishedConnections(_establishedConnections);
        }

        public void UnlinkAccount(string gitIdentifier)
        {
            foreach (var link in _establishedConnections)
            {
                if (link.GitUniqueIdentifier != gitIdentifier) continue;
                _establishedConnections.Remove(link);
                DataUpdater.UpdateEstablishedConnections(_establishedConnections);
                return;
            }
        }
    }
}
