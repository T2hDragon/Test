using AutoMapper;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Repositories;
using DAL.App.EF.Mappers;
using DAL.App.EF.Repositories;
using DAL.Base.EF;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        protected IMapper Mapper;
        public AppUnitOfWork(AppDbContext uowDbContext, IMapper mapper) : base(uowDbContext)
        {
            Mapper = mapper;
        }

        public IPersonRepository Persons => GetRepository(() => new PersonRepository(UowDbContext, Mapper));
        
        public IContractRepository Contracts => GetRepository(() => new ContractRepository(UowDbContext, Mapper));

        public IContractCourseRepository ContractCourses => GetRepository(() => new ContractCourseRepository(UowDbContext, Mapper));

        public ICourseRepository Courses => GetRepository(() => new CourseRepository(UowDbContext, Mapper));

        public ICourseRequirementRepository CourseRequirements => GetRepository(() => new CourseRequirementRepository(UowDbContext, Mapper));

        public IDrivingSchoolRepository DrivingSchools => GetRepository(() => new DrivingSchoolRepository(UowDbContext, Mapper));

        public ILessonRepository Lessons => GetRepository(() => new LessonRepository(UowDbContext, Mapper));

        public ILessonCourseRequirementRepository LessonCourseRequirements => GetRepository(() => new LessonCourseRequirementRepository(UowDbContext, Mapper));

        public ILessonParticipantRepository LessonParticipants => GetRepository(() => new LessonParticipantRepository(UowDbContext, Mapper));


        public ILessonParticipantConfirmationRepository LessonParticipantConfirmations => GetRepository(() => new LessonParticipantConfirmationRepository(UowDbContext, Mapper));

        public ILessonParticipantNoteRepository LessonParticipantNotes => GetRepository(() => new LessonParticipantNoteRepository(UowDbContext, Mapper));

        public IRequirementRepository Requirements => GetRepository(() => new RequirementRepository(UowDbContext, Mapper));

        public ITitleRepository Titles => GetRepository(() => new TitleRepository(UowDbContext, Mapper));
        public IWorkHourRepository WorkHours => GetRepository(() => new WorkHourRepository(UowDbContext, Mapper));

        public IContactRepository Contacts => GetRepository(() => new ContactRepository(UowDbContext, Mapper));

        public IContactTypeRepository ContactTypes => GetRepository(() => new ContactTypeRepository(UowDbContext, Mapper));
        
        // stays as baserepo for testing
        public IBaseRepository<DAL.App.DTO.Simple> Simples =>
            GetRepository(() => 
                new BaseRepository<DAL.App.DTO.Simple, Domain.App.Simple, AppDbContext>(UowDbContext, new BaseMapper<DAL.App.DTO.Simple, Domain.App.Simple>(Mapper) ));
        
        public IBaseRepository<DAL.App.DTO.IntPkThing, int> IntPkThings =>
            GetRepository(() => 
                new BaseRepository<DAL.App.DTO.IntPkThing, Domain.App.IntPkThing, int, AppDbContext>(UowDbContext, new BaseMapper<DAL.App.DTO.IntPkThing, Domain.App.IntPkThing>(Mapper) ));
    }
    
}