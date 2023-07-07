using Microsoft.AspNetCore.Mvc;

namespace scada_back.Infrastructure.Feature.User;

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

    [HttpPost(Name = "Login")]
    public ActionResult<string> Login(LoginDto credentials)
    {
        return Ok(_userService.Login(credentials.Username, credentials.Password));
    }
    
}