
namespace DownWithTheTodoList.Ms.Users.Helpers;

public class UserMemoryCache : IUserMemoryCache
{
    private readonly ILoggerAdapter<UserMemoryCache> _logger;
    private readonly MemoryCache _cache;
    private readonly MemoryCacheEntryOptions _userCacheOptions;

    public UserMemoryCache(ILoggerAdapter<UserMemoryCache> logger) 
    {
        _logger = logger;

        _logger.LogDebug("Creating memory cache");

        _cache = new MemoryCache(
            new MemoryCacheOptions
            {
                SizeLimit = 10240,
                ExpirationScanFrequency = TimeSpan.FromMinutes(60),
                CompactionPercentage = .25
            });

        _userCacheOptions = new()
        {
            AbsoluteExpiration = DateTime.UtcNow.AddHours(24),
            SlidingExpiration = TimeSpan.FromHours(4),
            Size = 1,
        };

        _logger.LogDebug("Memory cache created");
    }

    public bool TryGetValue(Guid id, out UserResponse _)
    {
        _logger.LogDebug("Try to get cache for {A0}", id);
        return _cache.TryGetValue<UserResponse>(id, out _);
    }

    public UserResponse SetUser(UserResponse user)
    {
        _logger.LogDebug("Setting user {A0} in cache", user.Id);
        return _cache.Set<UserResponse>(user.Id, user, _userCacheOptions);
    }

    public void DeleteUser(Guid id)
    {
        _logger.LogDebug("Deleting {A0} from cache", id);
        _cache.Remove(id);
    }

}
