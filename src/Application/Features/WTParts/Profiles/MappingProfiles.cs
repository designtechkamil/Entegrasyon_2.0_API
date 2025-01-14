using Application.Features.WTParts.Queries.GetList;
using Application.Paging;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.WTParts.Profiles;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
		CreateMap<WTPart,GetListWTPartListItemDto>().ReverseMap();
		CreateMap<Paginate<WTPart>, GetListResponse<GetListWTPartListItemDto>>().ReverseMap();



		//Farklı class larda yani class ile DTO prop ları arasında isim farklılığı var ise gerekli eşleşştirmeyi böyle yapabilriz.
		//CreateMap<Model, GetListModelListItemDto>()
		//	.ForMember(destinationMember: c => c.BrandName, memberOptions: opt => opt.MapFrom(c => c.Brand.Name))
		//	.ForMember(destinationMember: c => c.FuelName, memberOptions: opt => opt.MapFrom(c => c.Fuel.Name))
		//	.ForMember(destinationMember: c => c.TransmissionName, memberOptions: opt => opt.MapFrom(c => c.Transmission.Name))
		//	.ReverseMap();
	}
}
