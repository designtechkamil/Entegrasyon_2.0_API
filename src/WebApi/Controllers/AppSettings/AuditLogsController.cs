﻿using Application.Features.Log.Queries.GetLogByDatetime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.AppSettings;

[Route("api/[controller]")]
[ApiController]
public class AuditLogsController : BaseController
{
	[HttpGet("by-date")]
	public async Task<IActionResult> GetLogsByDate([FromQuery] DateTime date)
	{
		var query = new GetLogByDatetimeQuery { TimeStamp = date };
		var result = await Mediator.Send(query);
		return Ok(result);
	}
}
