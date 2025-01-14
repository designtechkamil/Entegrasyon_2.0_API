using Application.Features.DatabaseManagement.Queries.GetById;
using Application.Features.Log.Queries.GetLogByDatetime;
using Application.Features.WindchillManagement.Queries.WtToken.GetList;
using Application.Features.WindchillManagement.Queries.WtUser.GetUsers;
using AutoMapper;
using Domain.Entities.LogSettings;
using Domain.Entities.WindchillEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Log.Profiles;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
		CreateMap<LogEntry, LogEntryDto>().ReverseMap();
		CreateMap<List<LogEntry>, GetLogByDatetimeResponse>()
			   .ForMember(dest => dest.Logs, opt => opt.MapFrom(src => src));
	}
}
