namespace DownWithTheTodoList.Core.Models;

public class User
{
    public Guid Id { get; init; }

    public string NickName { get; init; } = default!;

    public string Password { get; init; } = default!;

}
