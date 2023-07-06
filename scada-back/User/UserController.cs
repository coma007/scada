using Microsoft.AspNetCore.Mvc;

namespace scada_back.User;

[ApiController]
[Route("Api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet(Name = "Login")]
    public ActionResult<UserDto> Login(string username, string password)
    {
        return Ok(_userService.Login(username, password));
    }
    
}