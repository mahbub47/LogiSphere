using LogiSphere.Application.Features.Authentication;
using LogiSphere.Application.Features.Authentication.Models;
using LogiSphere.Host.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogiSphere.Host.Controllers;

[ApiController]
[Route("api/auth")]
[AllowAnonymous]
public class LoginController(IAuthenticationService serivce) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult> LoginUser([FromBody]UserLoginDto request)
    {
        var authenticationRequest = new AuthenticationRequest
        {
            Email = request.Email,
            Password = request.Password,
        };

        var result = await serivce.AuthenticateUserAsync(authenticationRequest);

        if (result.IsFailure) return Unauthorized();

        return Ok(new
        {
            token = result.Token
        });
    }
}
