using Microsoft.AspNetCore.Mvc;
using scada_back.Exception;

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
    public ActionResult<UserDTO> Login(string username, string password)
    {
        UserDTO user;
        try
        {
            user = _userService.Login(username, password);
        }
        catch (ObjectNotFound e)
        {
            return NotFound(e.Message);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }

        return user;
    }
    
}