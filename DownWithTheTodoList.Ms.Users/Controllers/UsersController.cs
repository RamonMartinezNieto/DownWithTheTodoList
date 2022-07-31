
using System.Diagnostics;

namespace DownWithTheTodoList.Ms.Users.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILoggerAdapter<UsersController> _logger;
    private readonly IUserService _userService;

    public UsersController(
        ILoggerAdapter<UsersController> logger,
        IUserService userService)
    {
        _logger = logger;
        _userService = userService;
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
        Stopwatch sw = new();
        sw.Start();
        _logger.LogInformation("Llamando al método GEt");

        try
        {
            var users = await _userService.GetByIdAsync(id);

            sw.Stop();
            _logger.LogInformation(sw.Elapsed.ToString());

            return Ok(users);
        }
        catch (KeyNotFoundException)
        {
            return NoContent();
        }
        catch 
        {
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostUsers(UserCreateRequest model)
    {
        try {

            var result = await _userService.CreateAsync(model.ToUser());

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

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> PutUsers(Guid id, UserUpdateRequest model) 
    {
        try
        {
            var result = await _userService.UpdateAsync(model.ToUser(id));
            
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