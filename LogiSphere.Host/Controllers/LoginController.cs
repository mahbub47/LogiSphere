using LogiSphere.Application.Features.Authentication.Queries;
using LogiSphere.Host.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogiSphere.Host.Controllers;

[ApiController]
[Route("api/auth")]
[AllowAnonymous]
public class LoginController(ISender _sender) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult> LoginUser([FromBody] UserLoginDto request)
    {
        var loginCommand = new GetJwtTokenQuery
        {
            Email = request.Email,
            Password = request.Password,
        };

        var result = await _sender.Send(loginCommand, default);

        if (result!.IsFailure) return Unauthorized();

        return Ok(new
        {
            token = result.Value
        });
    }
}
