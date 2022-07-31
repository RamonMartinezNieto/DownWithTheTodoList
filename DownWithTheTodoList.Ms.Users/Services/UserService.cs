namespace DownWithTheTodoList.Ms.Users.Services;

public class UserService : IUserService
{
    private readonly ILoggerAdapter<UserService> _logger;
    private readonly IUserRepository _repository;
    private readonly IUserMemoryCache _userCache;

    public UserService(
        IUserRepository repository, 
        IUserMemoryCache userCache,
        ILoggerAdapter<UserService> logger) 
    {
        _repository = repository;
        _userCache = userCache;
        _logger = logger;
    }

    public async Task<UserResponse> CreateAsync(User user)
    {
        _logger.LogDebug("Creating user with name {A0}", user.NickName);

        try
        {
            var response = await _repository.CreateAsync(user);
            var userResponse = response.ToUserResponse();

            _userCache.SetUser(userResponse);

            _logger.LogDebug("User created with id {A0}", user.Id);

            return userResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user with name {A1}", user.NickName);
            throw;
        }
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        _logger.LogDebug("Start deleting user");

        try
        {
            var result = await _repository.DeleteByIdAsync(id);

            Ensure.That(result).IsTrue<KeyNotFoundException>($"Not found any item with id {id}");

            _userCache.DeleteUser(id);

            _logger.LogDebug("User deleted: {A0}",result);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting user with id {A0}", id);
            throw;
        }
    }

    public async Task<IEnumerable<UserResponse>> GetAllAsync()
    {
        _logger.LogDebug("Retrieving all users");

        try
        {
            var result = await _repository.GetAllAsync();

            var convertResult = result.Select(x => x.ToUserResponse());

            _logger.LogDebug("Users founded: {A0}", result.Count());

            return convertResult;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all users");
            throw;
        }
    }

    public async Task<UserResponse?> GetByIdAsync(Guid id)
    {
        _logger.LogDebug("Retrieving user with id: {A0}", id);

        try
        {
            User? user;

            if (!_userCache.TryGetValue(id, out UserResponse? result))
            {
                user = await _repository.GetByIdAsync(id);
                Ensure.That(user).IsNotNull<KeyNotFoundException>("There isn't any User with this id");
                result = user!.ToUserResponse();
            }

            Ensure.That(result).IsNotNull<KeyNotFoundException>("There isn't any User with this id");
            Ensure.That(result).IsNotDefault<UserResponse, KeyNotFoundException>("There isn't any User with this id");

            _userCache.SetUser(result);
            _logger.LogDebug("User found: {A0}", result?.NickName);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving the user with id {A0}", id);
            throw;
        }
    }

    public async Task<UserResponse> UpdateAsync(User user)
    {
        _logger.LogDebug("Updating user with id {A0} and name {A1}", user.Id, user.NickName);

        try
        {
            var userResponse = await _repository.UpdateAsync(user);

            Ensure.That(userResponse).IsNotNull<KeyNotFoundException>("Error updating user");

            _logger.LogDebug("User updated with id {A0}", user.Id);

            var response = userResponse.ToUserResponse();
            
            _userCache.SetUser(response);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user with id {A0} and name {A1}", user.Id, user.NickName);
            throw;
        }
    }
}