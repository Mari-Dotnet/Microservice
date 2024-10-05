
using AutoMapper;
using Service.CouponAPI.Models;
using Service.CouponAPI.Models.Dto;

namespace Service.CouponAPI.Mapper
{
    public class MappingConfig : Profile
    {

        public MappingConfig()
        {
            CreateMap<Coupon, CouponDto>().ReverseMap();
        }
        //public static MapperConfiguration RegisterMaps()
        //{
        //    var mapperConfig = new MapperConfiguration(config =>
        //    {
        //        config.CreateMap<Coupon, CouponDto>().ReverseMap();
        //    });
        //    return mapperConfig;
        //}

    }
}
