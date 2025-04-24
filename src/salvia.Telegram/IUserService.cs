using System.Threading.Tasks;

namespace salvia.Telegram;

public interface IUserService
{
    Task<bool> IsUserWhiteListed(long user);
}
