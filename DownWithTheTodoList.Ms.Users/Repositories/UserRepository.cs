namespace DownWithTheTodoList.Ms.Users.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UsersContext _context;

    public UserRepository(UsersContext context)
    {
        _context = context;
    }

    public async Task<User> CreateAsync(User user)
    {
        var userInserted = _context.Users.Add(user);
        var result = await _context.SaveChangesAsync();

        if (result > 0)
            return userInserted.Entity;

        throw new Exception("Some problem was occur");
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        User? user = _context.Users.Find(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }
        return false;
    }


    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await Task.Run(() => 
            _context.Users
                .Select(n => new User { Id = n.Id, NickName = n.NickName })
            );
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await Task.Run(() =>
            _context.Users
                .Where(u => u.Id.Equals(id))
                .Select(n => new User { Id = n.Id, NickName = n.NickName })
                .FirstOrDefaultAsync());
    }

    public async Task<User> UpdateAsync(User user)
    {
        _context.Entry(user).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return user;
    }
}