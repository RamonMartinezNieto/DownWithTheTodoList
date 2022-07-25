public class UserService : IUserService
{
    private readonly UsersContext _context;

    public UserService(UsersContext context) 
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
        if(user != null) { 
            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }
        return false;
    }


    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await Task.Run(() => _context.Users);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await Task.Run(() => _context.Users.Find(id));
    }

    public async Task<User> UpdateAsync(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
        
        var result = await _context.SaveChangesAsync();

        if (result > 0)
            return user;

        throw new Exception("Some problem was occur");
    }


    private bool _disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this._disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
