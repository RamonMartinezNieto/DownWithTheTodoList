namespace DownWithTheTodoList.Ms.Users.Helpers;

public interface IUserMemoryCache
{
    bool TryGetValue(Guid id, out UserResponse _);
    UserResponse SetUser(UserResponse user);
    void DeleteUser(Guid id);
}
