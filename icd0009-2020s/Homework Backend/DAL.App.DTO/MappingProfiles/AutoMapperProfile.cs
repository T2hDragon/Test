using AutoMapper;

namespace DAL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DAL.App.DTO.Contact, Domain.App.Contact>().ReverseMap();
            CreateMap<DAL.App.DTO.ContactType, Domain.App.ContactType>().ReverseMap();
            CreateMap<DAL.App.DTO.Person, Domain.App.Person>().ReverseMap();
            CreateMap<DAL.App.DTO.Simple, Domain.App.Simple>().ReverseMap();
            CreateMap<DAL.App.DTO.Identity.AppUser, Domain.App.Identity.AppUser>().ReverseMap();
            CreateMap<DAL.App.DTO.Identity.AppRole, Domain.App.Identity.AppRole>().ReverseMap();
        }
    }
}

