namespace DownWithTheTodoList.Ms.Users.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly UsersRepository _userRepository;

    public UsersController(ILogger<UsersController> logger, UsersRepository repository)
    {
        _logger = logger;
        _userRepository = repository;
    }

    [HttpGet]
    public Task<IEnumerable<User>> Get()
    {
        _logger.LogInformation("Llamando al método GEt");
        return _userRepository.GetAllAsync();
    }
}