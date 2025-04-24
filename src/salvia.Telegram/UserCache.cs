using System.Collections.Generic;

namespace salvia.Telegram;

internal class UserCache : IUserCache
{
    private readonly Dictionary<long, bool> _whiteListCache = new();

    public void AddInWhiteList(long user, bool isWhiteListed)
    {
        _whiteListCache[user] = isWhiteListed;
    }

    public bool? FindInWhiteList(long user)
    {
        if (_whiteListCache.TryGetValue(user, out var result))
        {
            return result;
        }

        return null;
    }
}
