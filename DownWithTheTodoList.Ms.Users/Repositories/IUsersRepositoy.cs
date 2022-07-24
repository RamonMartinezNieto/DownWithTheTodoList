namespace DownWithTheTodoList.Ms.Users.Repositories;

public interface IUsersRepositoy
{
    Task<IEnumerable<User>> GetAllAsync();

    Task<User> GetByIdAsync();

    Task<bool> CreateAsync(User user);

    Task<bool> DeleteByIdAsync(Guid user);
}
