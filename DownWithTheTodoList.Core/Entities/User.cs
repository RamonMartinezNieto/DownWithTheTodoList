namespace DownWithTheTodoList.Core.Models;

public class User
{
    public Guid Id { get; set; }

    public string NickName { get; init; } = default!;

    public string Password { get; init; } = default!;

}
