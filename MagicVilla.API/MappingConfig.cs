using AutoMapper;
using MagicVilla.API.Models;
using MagicVilla.API.Models.DataTransferObjects;

namespace MagicVilla.API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDto>().ReverseMap();
            CreateMap<Villa, VillaCreateDto>().ReverseMap();
            CreateMap<Villa, VillaUpdateDto>().ReverseMap();
        }
    }
}
