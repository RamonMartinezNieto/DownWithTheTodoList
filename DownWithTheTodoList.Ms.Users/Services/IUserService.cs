namespace DownWithTheTodoList.Ms.Users.Services;

public interface IUserService
{
    Task<IEnumerable<UserResponse>> GetAllAsync();
    Task<UserResponse?> GetByIdAsync(Guid id);
    Task<UserResponse> CreateAsync(User user);
    Task<UserResponse> UpdateAsync(User user);
    Task<bool> DeleteByIdAsync(Guid user);
}
