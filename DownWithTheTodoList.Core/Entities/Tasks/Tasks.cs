namespace DownWithTheTodoList.Core.Models.Tasks;

public class Tasks
{
    public Guid Id { get; init; }

    public Guid IdUser { get; init; } 

    public Guid? IdBlock { get; set; }

    public string Title { get; init; } = default!;

    public string? Description { get; set; } = default!;

}
