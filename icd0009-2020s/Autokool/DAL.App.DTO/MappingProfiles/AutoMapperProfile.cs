using AutoMapper;

namespace DAL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DAL.App.DTO.Title, Domain.App.Title>().ReverseMap();
            CreateMap<DAL.App.DTO.DrivingSchool, Domain.App.DrivingSchool>().ReverseMap();
            CreateMap<DAL.App.DTO.Course, Domain.App.Course>().ReverseMap();
            CreateMap<DAL.App.DTO.ContractCourse, Domain.App.ContractCourse>().ReverseMap();
            CreateMap<DAL.App.DTO.Contract, Domain.App.Contract>().ReverseMap();
            CreateMap<DAL.App.DTO.CourseRequirement, Domain.App.CourseRequirement>().ReverseMap();
            CreateMap<DAL.App.DTO.Lesson, Domain.App.Lesson>().ReverseMap();
            CreateMap<DAL.App.DTO.LessonParticipant, Domain.App.LessonParticipant>().ReverseMap();
            CreateMap<DAL.App.DTO.Requirement, Domain.App.Requirement>().ReverseMap();
            CreateMap<DAL.App.DTO.Title, Domain.App.Title>().ReverseMap();
            CreateMap<DAL.App.DTO.Status, Domain.App.Status>().ReverseMap();
            CreateMap<DAL.App.DTO.Identity.AppUser, Domain.App.Identity.AppUser>().ReverseMap();
            CreateMap<DAL.App.DTO.Identity.AppRole, Domain.App.Identity.AppRole>().ReverseMap();
        }
    }
}

