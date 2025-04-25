namespace salvia.Telegram;

public interface IUserCache
{
    bool InBan(long user);
    void AddInBan(long user);
}
