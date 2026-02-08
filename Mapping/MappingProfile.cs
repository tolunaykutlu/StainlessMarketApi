using AutoMapper;
using StainlessMarketApi.Dtos;
using StainlessMarketApi.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<StokProductEntity, StokProductDto>().ReverseMap();

        CreateMap<FasonProductEntity, FasonProductDto>().ReverseMap();
       // CreateMap<UserEntity, UserDto>().ReverseMap();
    }
}