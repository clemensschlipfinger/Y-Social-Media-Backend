using Microsoft.AspNetCore.Mvc;

namespace Backend.Identity;

public record UserInfo(string? Username, string? Password);

[ApiController]
[Route("token")]
public class JwtController : ControllerBase
{
    private readonly IJwtService _jwtService;
    private readonly IUserService _userService;

    public JwtController(IJwtService jwtService, IUserService userService)
    {
        _jwtService = jwtService;
        _userService = userService;
    }
    
    [HttpPost]
    public async Task<IActionResult> GetToken(UserInfo userInfo)
    { 
        var storedUser = await _userService.GetUser(userInfo?.Username);
        if (storedUser == null)
            return Unauthorized();
        
        if (!_userService.IsAuthenticated(userInfo.Password, storedUser))
            return Unauthorized();

        var tokenString = _jwtService.GenerateToken(storedUser);
        return new JsonResult(new { token = tokenString });
    }
}
