using Microsoft.Extensions.Options;
using salvia.Data.Entities;
using salvia.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace salvia.Telegram;

internal class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly BotConfiguration _botConfig;
    private readonly IUserCache _userCache;


    public UserService(IUserRepository userRepository, IOptions<BotConfiguration> botConfig, IUserCache userCache)
    {
        _userRepository = userRepository;
        _botConfig = botConfig.Value;
        _userCache = userCache;
    }


    public async Task<bool> IsUserWhiteListed(long user)
    {
        var cached = _userCache.FindInWhiteList(user);
        if (cached.HasValue)
        {
            return cached.Value;
        }

        var userDto = await _userRepository.TryGetItem(user);
        if (userDto is null)
        {
            userDto = new UserDto()
            {
                Id = user,
                IsWhiteListed = false,
                StartUsing = DateTime.Now,
            };

            await _userRepository.Create(userDto);
            await _userRepository.Save();
        }

        if (_botConfig.UseWhiteList)
        {
            _userCache.AddInWhiteList(user, userDto.IsWhiteListed);

            if (!userDto.IsWhiteListed)
            {
                return false;
            }
        }

        return true;
    }
}
