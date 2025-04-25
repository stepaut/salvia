using salvia.Data.Entities;
using System.Threading.Tasks;

namespace salvia.Telegram;

public interface IUserService
{
    Task<UserDto> GetUserInfo(long user);
}
