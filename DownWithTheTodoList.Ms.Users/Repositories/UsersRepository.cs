
namespace DownWithTheTodoList.Ms.Users.Repositories;

public class UsersRepository : IUsersRepositoy
{
    private UsersContext _context;

    public UsersRepository(UsersContext context) 
    {
        this._context = context;
    }

    public Task<bool> CreateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteByIdAsync(Guid user)
    {
        throw new NotImplementedException();
    }


    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return _context.Users;
    }

    public Task<User> GetByIdAsync()
    {
        throw new NotImplementedException();
    }
}
