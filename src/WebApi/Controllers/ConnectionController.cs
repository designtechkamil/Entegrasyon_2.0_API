using Application.Features.Connection.Sql.Commands.Update;
using Application.Features.Connection.Sql.Queries.GetList;
using Application.Features.Connection.Sql.Queries.SqlContorls;
using Application.Features.Connection.Windchill.Commands.Update;
using Application.Features.Connection.Windchill.Queries.GetList;
using Application.Features.Users.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConnectionController : BaseController
{
	private readonly IConfiguration _configuration;

	public ConnectionController(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	[HttpGet]
	public async Task<IActionResult> GetConnectionString()
	{

		GetListConnectionQuery getListConnectionQuery = new();
		GetListConnectionListItemDto response = await Mediator.Send(getListConnectionQuery);
		return Ok(response);
	}


	[HttpGet("ConnectionControl")]
	public async Task<IActionResult> GetConnectionControl()
	{
		ConnectionControlQuery connectionControlQuery = new();
		bool response = await Mediator.Send(connectionControlQuery);
		return Ok(response);
	}


	[HttpPut]
	public async Task<IActionResult> UpdateConnectionString([FromBody] UpdateConnectionCommand updateConnectionCommand)
	{
		UpdatedConnectionResponse response = await Mediator.Send(updateConnectionCommand);

		return Ok(response);

	}


	[HttpGet("WindchillConnection")]
	public async Task<IActionResult> GetWindchillConnectionString()
	{

		GetListWindchillConnectionQuery getListConnectionQuery = new();
		GetListWindchillConnectionListItemDto response = await Mediator.Send(getListConnectionQuery);
		return Ok(response);

		throw new NotImplementedException();

	}


	[HttpPut("WindchillConnection")]
	public async Task<IActionResult> UpdateWindchillConnectionString([FromBody] UpdateWindchillConnectionCommand updateWindchillConnectionCommand)
	{
		UpdatedWindchillConnectionResponse response = await Mediator.Send(updateWindchillConnectionCommand);
		return Ok(response);

	}



}
