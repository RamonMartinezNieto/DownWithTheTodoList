﻿namespace DownWithTheTodoList.Ms.Users.Services;

public class UserService : IUserService
{
    private readonly ILoggerAdapter<UserService> _logger;
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository, ILoggerAdapter<UserService> logger) 
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<UserResponse> CreateAsync(User user)
    {
        _logger.LogDebug("Creating user with name {A0}", user.NickName);

        try
        {
            var userResponse = await _repository.CreateAsync(user);

            _logger.LogDebug("User created with id {A0}", user.Id);

            return userResponse.ToUserResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user with name {A1}", user.NickName);
            throw;
        }
        finally 
        {
            _repository.Dispose();
        }
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        _logger.LogDebug("Start deleting user");

        try
        {
            var result = await _repository.DeleteByIdAsync(id);

            Ensure.That(result).IsTrue<KeyNotFoundException>($"Not found any item with id {id}");

            _logger.LogDebug("User deleted: {A0}",result);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting user with id {A0}", id);
            throw;
        }
        finally
        {
            _repository.Dispose();
        }
    }


    public async Task<IEnumerable<User>> GetAllAsync()
    {
        _logger.LogDebug("Retrieving all users");

        try
        {
            var result = await _repository.GetAllAsync();

            _logger.LogDebug("Users founded: {A0}", result.Count());

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all users");
            throw;
        }
        finally
        {
            _repository.Dispose();
        }
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        _logger.LogDebug("Retrieving user with id: {A0}", id);

        try
        {
            var result = await _repository.GetByIdAsync(id);

            Ensure.That(result).IsNotNull<KeyNotFoundException>("There isn't any User with this id");
            
            _logger.LogDebug("User found: {A0}", result?.NickName);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving the user with id {A0}", id);
            throw;
        }
        finally
        {
            _repository.Dispose();
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

            return userResponse.ToUserResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user with id {A0} and name {A1}", user.Id, user.NickName);
            throw;
        }
        finally
        {
            _repository.Dispose();
        }
    }

}