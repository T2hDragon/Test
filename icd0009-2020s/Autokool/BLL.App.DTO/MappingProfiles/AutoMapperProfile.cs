using AutoMapper;
using BLL.App.DTO.Identity;

namespace BLL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Contract, DAL.App.DTO.Contract>().ReverseMap();
            CreateMap<ContractCourse, DAL.App.DTO.ContractCourse>().ReverseMap();
            CreateMap<CourseRequirement, DAL.App.DTO.CourseRequirement>().ReverseMap();
            CreateMap<Course, DAL.App.DTO.Course>().ReverseMap();
            CreateMap<DrivingSchool, DAL.App.DTO.DrivingSchool>().ReverseMap();
            CreateMap<LessonParticipant, DAL.App.DTO.LessonParticipant>().ReverseMap();
            CreateMap<Lesson, DAL.App.DTO.Lesson>().ReverseMap();
            CreateMap<Requirement, DAL.App.DTO.Requirement>().ReverseMap();
            CreateMap<Title, DAL.App.DTO.Title>().ReverseMap();
            CreateMap<AppUser, DAL.App.DTO.Identity.AppUser>().ReverseMap();
            CreateMap<AppRole, DAL.App.DTO.Identity.AppRole>().ReverseMap();
            CreateMap<Status, DAL.App.DTO.Status>().ReverseMap();
        }
    }
}

