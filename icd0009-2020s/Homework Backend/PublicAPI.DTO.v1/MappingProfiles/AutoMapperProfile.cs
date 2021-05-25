using AutoMapper;

namespace PublicAPI.DTO.v1.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BLL.App.DTO.Person, PublicAPI.DTO.v1.PersonAdd>().ReverseMap();
            CreateMap<BLL.App.DTO.Person, PublicAPI.DTO.v1.Person>().ReverseMap();

        }
    }
}

