using AutoMapper;
using BLL.App.DTO.Identity;

namespace BLL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<AppUser, DAL.App.DTO.Identity.AppUser>().ReverseMap();
            CreateMap<AppRole, DAL.App.DTO.Identity.AppRole>().ReverseMap();
            CreateMap<Answer, DAL.App.DTO.Answer>().ReverseMap();
            CreateMap<Participant, DAL.App.DTO.Participant>().ReverseMap();
            CreateMap<PickedAnswer, DAL.App.DTO.PickedAnswer>().ReverseMap();
            CreateMap<Question, DAL.App.DTO.Question>().ReverseMap();
            CreateMap<Quiz, DAL.App.DTO.Quiz>().ReverseMap();
        }
    }
}

