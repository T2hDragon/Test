using System;
using AutoMapper;
using BLL.App.Mappers;
using BLL.App.Services;
using BLL.Base;
using BLL.Base.Services;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App;
using Contracts.DAL.Base.Repositories;
using Domain.App;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        protected IMapper Mapper;
        public AppBLL(IAppUnitOfWork uow, IMapper mapper) : base(uow)
        {
            Mapper = mapper;
        }


        public IPersonService Persons =>
            GetService<IPersonService>(() => new PersonService(Uow, Uow.Persons, Mapper));
        
        public IContactService Contacts =>
            GetService<IContactService>(() => new ContactService(Uow, Uow.Contacts, Mapper));

        public IContactTypeService ContactTypes =>
            GetService<IContactTypeService>(() => new ContactTypeService(Uow, Uow.ContactTypes, Mapper));
        public IContractService Contracts =>
            GetService<IContractService>(() => new ContractService(Uow, Uow.Contracts, Mapper));
        
        public IContractCourseService ContractCourses =>
            GetService<IContractCourseService>(() => new ContractCourseService(Uow, Uow.ContractCourses, Mapper));
        public ICourseService Courses  =>
            GetService<ICourseService>(() => new CourseService(Uow, Uow.Courses, Mapper));
        public ICourseRequirementService CourseRequirements  =>
            GetService<ICourseRequirementService>(() => new CourseRequirementService(Uow, Uow.CourseRequirements, Mapper));
        public IDrivingSchoolService DrivingSchools =>
            GetService<IDrivingSchoolService>(() => new DrivingSchoolService(Uow, Uow.DrivingSchools, Mapper));
        public ILessonService Lessons  =>
            GetService<ILessonService>(() => new LessonService(Uow, Uow.Lessons, Mapper));
        public ILessonCourseRequirementService LessonCourseRequirements =>
            GetService<ILessonCourseRequirementService>(() => new LessonCourseRequirementService(Uow, Uow.LessonCourseRequirements, Mapper));
        public ILessonParticipantService LessonParticipants  =>
            GetService<ILessonParticipantService>(() => new LessonParticipantService(Uow, Uow.LessonParticipants, Mapper));
        public ILessonParticipantConfirmationService LessonParticipantConfirmations  =>
            GetService<ILessonParticipantConfirmationService>(() => new LessonParticipantConfirmationService(Uow, Uow.LessonParticipantConfirmations, Mapper));
        public ILessonParticipantNoteService LessonParticipantNotes  =>
            GetService<ILessonParticipantNoteService>(() => new LessonParticipantNoteService(Uow, Uow.LessonParticipantNotes, Mapper));
        public IRequirementService Requirements =>
            GetService<IRequirementService>(() => new RequirementService(Uow, Uow.Requirements, Mapper));
        public ITitleService Titles =>
            GetService<ITitleService>(() => new TitleService(Uow, Uow.Titles, Mapper));
        public IWorkHourService WorkHours =>
            GetService<IWorkHourService>(() => new WorkHourService(Uow, Uow.WorkHours, Mapper));
        public IBaseEntityService<BLL.App.DTO.Simple, DAL.App.DTO.Simple> Simples =>
            GetService<IBaseEntityService<BLL.App.DTO.Simple, DAL.App.DTO.Simple>>(()
                => new BaseEntityService<IAppUnitOfWork,
                    IBaseRepository<DAL.App.DTO.Simple>, BLL.App.DTO.Simple, DAL.App.DTO.Simple>(Uow, Uow.Simples, new BaseMapper<BLL.App.DTO.Simple, DAL.App.DTO.Simple>(Mapper)));
        
        public IBaseEntityService<BLL.App.DTO.IntPkThing, DAL.App.DTO.IntPkThing, int> IntPkThings =>
            GetService<IBaseEntityService<BLL.App.DTO.IntPkThing, DAL.App.DTO.IntPkThing, int>>(()
                => new BaseEntityService<IAppUnitOfWork,
                    IBaseRepository<DAL.App.DTO.IntPkThing, int>, BLL.App.DTO.IntPkThing, DAL.App.DTO.IntPkThing, int>(Uow, Uow.IntPkThings, new BaseMapper<BLL.App.DTO.IntPkThing, DAL.App.DTO.IntPkThing>(Mapper)));
        
    }
    
}