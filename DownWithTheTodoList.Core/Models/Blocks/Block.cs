namespace DownWithTheTodoList.Core.Models.Blocks;

public class Block
{
    public Guid Id { get; init; }

    public Guid IdUser { get; init; }

    public string Title { get; init; } = default!;

    public string? Description { get; set; } = default!;

}
