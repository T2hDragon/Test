using AutoMapper;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Repositories;
using DAL.App.EF.Mappers;
using DAL.App.EF.Repositories;
using DAL.Base.EF;
using DAL.Base.EF.Repositories;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        protected IMapper Mapper;
        public AppUnitOfWork(AppDbContext uowDbContext, IMapper mapper) : base(uowDbContext)
        {
            Mapper = mapper;
        }
        
        public IContractRepository Contracts => GetRepository(() => new ContractRepository(UowDbContext, Mapper));

        public IContractCourseRepository ContractCourses => GetRepository(() => new ContractCourseRepository(UowDbContext, Mapper));

        public ICourseRepository Courses => GetRepository(() => new CourseRepository(UowDbContext, Mapper));

        public ICourseRequirementRepository CourseRequirements => GetRepository(() => new CourseRequirementRepository(UowDbContext, Mapper));

        public IDrivingSchoolRepository DrivingSchools => GetRepository(() => new DrivingSchoolRepository(UowDbContext, Mapper));

        public ILessonRepository Lessons => GetRepository(() => new LessonRepository(UowDbContext, Mapper));

        public ILessonParticipantRepository LessonParticipants => GetRepository(() => new LessonParticipantRepository(UowDbContext, Mapper));
        

        public IRequirementRepository Requirements => GetRepository(() => new RequirementRepository(UowDbContext, Mapper));
        
        public IStatusRepository Statuses => GetRepository(() => new StatusRepository(UowDbContext, Mapper));
        
        public IBaseRepository<DAL.App.DTO.Title> Titles =>
            GetRepository(() => 
                new BaseRepository<DAL.App.DTO.Title, Domain.App.Title, AppDbContext>(UowDbContext, new BaseMapper<DAL.App.DTO.Title, Domain.App.Title>(Mapper) ));
        
    }
    
}