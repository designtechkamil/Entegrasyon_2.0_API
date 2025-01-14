using Application.Features.Auth.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Security.Entities;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController
{



	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginCommand command)
	{
		var response = await Mediator.Send(command);
		return Ok(response);
	}


	[HttpGet("validate-token")]
	public IActionResult ValidateToken()
	{
		// Token'ın geçerliliği otomatik olarak JWT middleware tarafından kontrol edilir
		return Ok(new { isValid = true });
	}

}
