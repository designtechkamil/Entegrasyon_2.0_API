using Application.Interfaces.LogModule;
using Application.Pipelines.Logging;
using AutoMapper;
using Domain.Entities.LogSettings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Log.Queries.GetLogByDatetime;

public class GetLogByDatetimeQuery : IRequest<GetLogByDatetimeResponse>/*,ILoggableRequest*/
{
	public DateTime TimeStamp { get; set; }

	//public string LogMessage { get; set; }
	public class GetLogByDatetimeQueryHandler : IRequestHandler<GetLogByDatetimeQuery, GetLogByDatetimeResponse>
	{
		private readonly IMapper _mapper;
		private readonly ILogService _logService;

		public GetLogByDatetimeQueryHandler(IMapper mapper, ILogService logService)
		{
			_mapper = mapper;
			_logService = logService;
		}

		public async Task<GetLogByDatetimeResponse> Handle(GetLogByDatetimeQuery request, CancellationToken cancellationToken)
		{
			var logEntries = await _logService.GetLogsByDateAsync(request.TimeStamp);
			var response = _mapper.Map<GetLogByDatetimeResponse>(logEntries);
			//request.LogMessage = $"{request.TimeStamp} tarihine göre loglar listelendi";
			return response;
		}
	}
}
