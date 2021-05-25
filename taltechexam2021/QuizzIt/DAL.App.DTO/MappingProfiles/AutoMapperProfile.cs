using AutoMapper;

namespace DAL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DAL.App.DTO.Answer, Domain.App.Answer>().ReverseMap();
            CreateMap<DAL.App.DTO.Participant, Domain.App.Participant>().ReverseMap();
            CreateMap<DAL.App.DTO.PickedAnswer, Domain.App.PickedAnswer>().ReverseMap();
            CreateMap<DAL.App.DTO.Question, Domain.App.Question>().ReverseMap();
            CreateMap<DAL.App.DTO.Quiz, Domain.App.Quiz>().ReverseMap();
            CreateMap<DAL.App.DTO.Identity.AppUser, Domain.App.Identity.AppUser>().ReverseMap();
            CreateMap<DAL.App.DTO.Identity.AppRole, Domain.App.Identity.AppRole>().ReverseMap();
        }
    }
}

