using System.Collections.Generic;

namespace salvia.Telegram;

internal class UserCache : IUserCache
{
    private readonly HashSet<long> _bansCache = new();

    public void AddInBan(long user)
    {
        _bansCache.Add(user);
    }

    public bool InBan(long user)
    {
        return _bansCache.Contains(user);
    }
}
