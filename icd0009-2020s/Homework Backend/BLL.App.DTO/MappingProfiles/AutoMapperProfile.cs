using AutoMapper;
using BLL.App.DTO.Identity;

namespace BLL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Contact, DAL.App.DTO.Contact>().ReverseMap();
            CreateMap<ContactType, DAL.App.DTO.ContactType>().ReverseMap();
            CreateMap<Person, DAL.App.DTO.Person>().ReverseMap();
            CreateMap<Contract, DAL.App.DTO.Contract>().ReverseMap();
            CreateMap<ContractCourse, DAL.App.DTO.ContractCourse>().ReverseMap();
            CreateMap<CourseRequirement, DAL.App.DTO.CourseRequirement>().ReverseMap();
            CreateMap<Course, DAL.App.DTO.Course>().ReverseMap();
            CreateMap<DrivingSchool, DAL.App.DTO.DrivingSchool>().ReverseMap();
            CreateMap<LessonCourseRequirement, DAL.App.DTO.LessonCourseRequirement>().ReverseMap();
            CreateMap<LessonParticipantConfirmation, DAL.App.DTO.LessonParticipantConfirmation>().ReverseMap();
            CreateMap<LessonParticipantNote, DAL.App.DTO.LessonParticipantNote>().ReverseMap();
            CreateMap<LessonParticipant, DAL.App.DTO.LessonParticipant>().ReverseMap();
            CreateMap<Lesson, DAL.App.DTO.Lesson>().ReverseMap();
            CreateMap<Requirement, DAL.App.DTO.Requirement>().ReverseMap();
            CreateMap<WorkHour, DAL.App.DTO.WorkHour>().ReverseMap();
            CreateMap<Simple, DAL.App.DTO.Simple>().ReverseMap();
            CreateMap<Simple, DAL.App.DTO.IntPkThing>().ReverseMap();
            CreateMap<AppUser, DAL.App.DTO.Identity.AppUser>().ReverseMap();
            CreateMap<AppRole, DAL.App.DTO.Identity.AppRole>().ReverseMap();
        }
    }
}

