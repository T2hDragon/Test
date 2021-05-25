using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        IPersonRepository Persons { get; }
        
        IContactRepository Contacts { get; } 
        IContactTypeRepository ContactTypes { get; } 

        IContractRepository Contracts { get; }
        IContractCourseRepository ContractCourses { get; } 
        ICourseRepository Courses { get; } 
        ICourseRequirementRepository CourseRequirements { get; } 
        IDrivingSchoolRepository DrivingSchools { get; } 
        ILessonRepository Lessons { get; } 
        ILessonCourseRequirementRepository LessonCourseRequirements { get; } 
        ILessonParticipantRepository LessonParticipants { get; } 
        ILessonParticipantConfirmationRepository LessonParticipantConfirmations { get; } 
        ILessonParticipantNoteRepository LessonParticipantNotes { get; } 
        IRequirementRepository Requirements { get; } 
        IWorkHourRepository WorkHours { get; } 
        ITitleRepository Titles { get; } 
        IBaseRepository<Simple> Simples { get; }
        IBaseRepository<IntPkThing, int> IntPkThings { get; }

    }
}