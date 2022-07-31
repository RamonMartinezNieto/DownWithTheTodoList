namespace DownWithTheTodoList.Ms.Users.Models;

public class UserResponse
{
    public Guid Id { get; set; }
    public string NickName { get; init; } = default!;   
}
