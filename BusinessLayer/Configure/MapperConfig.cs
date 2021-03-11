using AutoMapper;
using BusinessLayer.ViewModels;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Configure
{
    public static class MapperConfig
    {
        
        public static IMapper Mapper { get; set; }
        static MapperConfig()
        {
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<Car, CarVM>().ReverseMap();
                    cfg.CreateMap<ApplicationUserIdentity, LoginVM>().ReverseMap();
                    cfg.CreateMap<ApplicationUserIdentity, RegisterVM>()
                    .ForMember(dst => dst.City, src => src.MapFrom(u => u.Address.City))
                    .ForMember(dst => dst.Direction, src => src.MapFrom(u => u.Address.Direction))
                    .ForMember(dst => dst.Street, src => src.MapFrom(u => u.Address.Street))
                    .ReverseMap();

                });
            Mapper = config.CreateMapper();
        }
    }
}
