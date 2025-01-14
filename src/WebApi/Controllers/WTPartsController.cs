using Application.Features.WTParts.Queries.GetList;
using Application.Requests;
using Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WTPartsController : BaseController
{
	//[HttpGet] Eski
	//public async Task<IActionResult> GetList()
	//{

	//	GetListWTPartQuery getListWTPartQuery = new();
	//	List<GetListWTPartListItemDto> response = await Mediator.Send(getListWTPartQuery);
	//	return Ok(response);
	//}	

	//[HttpGet]
	//public async Task<IActionResult> GetList([FromQuery] PageRequest pageResult)
	//{
	//	GetListWTPartQuery getListWTPartQuery = new() { PageRequest = pageResult };
	//	GetListResponse<GetListWTPartListItemDto> response = await Mediator.Send(getListWTPartQuery);
	//	return Ok(response);
	//}
}
