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

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        protected IMapper Mapper;
        public AppBLL(IAppUnitOfWork uow, IMapper mapper) : base(uow)
        {
            Mapper = mapper;
        }
        
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
        public ILessonParticipantService LessonParticipants  =>
            GetService<ILessonParticipantService>(() => new LessonParticipantService(Uow, Uow.LessonParticipants, Mapper));
        public IRequirementService Requirements =>
            GetService<IRequirementService>(() => new RequirementService(Uow, Uow.Requirements, Mapper));
        public IStatusService Statuses =>
            GetService<IStatusService>(() => new StatusService(Uow, Uow.Statuses, Mapper));
        
                
        public IBaseEntityService<BLL.App.DTO.Title, DAL.App.DTO.Title> Titles =>
            GetService<IBaseEntityService<BLL.App.DTO.Title, DAL.App.DTO.Title>>(()
                => new BaseEntityService<IAppUnitOfWork,
                    IBaseRepository<DAL.App.DTO.Title>, BLL.App.DTO.Title, DAL.App.DTO.Title>(Uow, Uow.Titles, new BaseMapper<BLL.App.DTO.Title, DAL.App.DTO.Title>(Mapper)));

    }
    
}