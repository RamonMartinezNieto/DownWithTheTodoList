namespace DownWithTheTodoList.Ms.Users.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<UserResponse> CreateAsync(User user);
    Task<UserResponse> UpdateAsync(User user);
    Task<bool> DeleteByIdAsync(Guid user);
}
