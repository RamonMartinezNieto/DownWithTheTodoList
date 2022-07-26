namespace DownWithTheTodoList.Ms.Users.Repositories;

public interface IUserRepository : IDisposable
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<User> CreateAsync(User user);
    Task<User> UpdateAsync(User user);
    Task<bool> DeleteByIdAsync(Guid user);
}
