namespace salvia.Telegram;

public interface IUserCache
{
    bool? FindInWhiteList(long user);
    void AddInWhiteList(long user, bool isWhiteListed);
}
