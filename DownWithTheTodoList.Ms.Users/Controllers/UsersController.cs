
namespace DownWithTheTodoList.Ms.Users.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILoggerAdapter<UsersController> _logger;
    private readonly IUserRepository _userRepository;

    public UsersController(
        ILoggerAdapter<UsersController> logger,
        IUserRepository repository)
    {
        _logger = logger;
        _userRepository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("Llamando al método GEt");

        try
        {
            var users = await _userRepository.GetAllAsync();

            if (users.Any())
                return Ok(users);

            return NoContent();

        }
        catch {

            return StatusCode(500);
        }
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        _logger.LogInformation("Llamando al método GEt");

        try
        {
            var users = await _userRepository.GetByIdAsync(id);

            if (users is not null)
                return Ok(users);

            return NoContent();

        }
        catch {

            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostUsers(UserCreateRequest model)
    {
        try {

            User result = await _userRepository.CreateAsync(model.ToUser());

            return CreatedAtAction(
                nameof(Get),
                new { id = result.Id },
                result);
        }
        catch
        {
            return StatusCode(500);
        }
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteUsers(Guid id)
    {
        try
        {
            var result = await _userRepository.DeleteByIdAsync(id);

            if (result)
                return Ok();

            return NoContent();
        }
        catch
        {
            return StatusCode(500);
        }
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> PutUsers(Guid id, UserUpdateRequest model) 
    {
        try
        {
            User result = await _userRepository.UpdateAsync(model.ToUser(id));
            
            if(result is not null)
                return Ok();

            return NotFound();
        }
        catch
        {
            return StatusCode(500);
        }
    }

}