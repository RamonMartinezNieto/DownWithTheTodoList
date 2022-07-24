namespace DownWithTheTodoList.Ms.Users.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserService _userService;

    public UsersController(
        ILogger<UsersController> logger,
        IUserService service)
    {
        _logger = logger;
        _userService = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("Llamando al método GEt");

        try
        {
            var users = await _userService.GetAllAsync();

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
            var users = await _userService.GetByIdAsync(id);

            if (users is not null)
                return Ok(users);

            return NoContent();

        }
        catch {

            return StatusCode(500);
        }
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(UserCreateRequest model)
    {
        try {

            User result = await _userService.CreateAsync(model.ToUser());

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

    [HttpDelete("Delete/{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var result = await _userService.DeleteByIdAsync(id);

            if (result)
                return Ok();

            return NoContent();
        }
        catch
        {
            return StatusCode(500);
        }
    }

    [HttpPut("Update/{id:Guid}")]
    public async Task<IActionResult> Update(Guid id, UserUpdateRequest model) 
    {
        try
        {
            User result = await _userService.UpdateAsync(model.ToUser(id));
            
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