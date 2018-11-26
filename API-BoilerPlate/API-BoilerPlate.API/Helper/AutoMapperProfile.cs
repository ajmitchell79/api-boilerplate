using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace API_BoilerPlate.API.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<DAL.Entities.Orders, BRL.Query.Order>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.LocationId, opt => opt.MapFrom(src => src.Location.Id))
            //    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderMenuItem));

            CreateMap<DAL.Entities.Orders, BRL.Query.Order>();
            CreateMap<BRL.Query.Order,DAL.Entities.Orders>();

            //shoes
            CreateMap<DAL.Entities.Shoes, BRL.Query.Shoes>();
            CreateMap<BRL.Query.Shoes,DAL.Entities.Shoes>();
        }
    }
}
