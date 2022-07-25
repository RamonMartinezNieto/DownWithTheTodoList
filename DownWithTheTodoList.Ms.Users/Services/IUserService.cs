namespace DownWithTheTodoList.Ms.Users.Services;

public interface IUserService : IDisposable
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<User> CreateAsync(User user);
    Task<User> UpdateAsync(User user);
    Task<bool> DeleteByIdAsync(Guid user);
}
