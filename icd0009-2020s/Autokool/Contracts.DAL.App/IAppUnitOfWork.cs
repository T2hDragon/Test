using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {

        IContractRepository Contracts { get; }
        IContractCourseRepository ContractCourses { get; } 
        ICourseRepository Courses { get; } 
        ICourseRequirementRepository CourseRequirements { get; } 
        IDrivingSchoolRepository DrivingSchools { get; } 
        ILessonRepository Lessons { get; } 
        ILessonParticipantRepository LessonParticipants { get; }
        IRequirementRepository Requirements { get; } 
        IStatusRepository Statuses { get; }
        IBaseRepository<Title> Titles { get; }
    }
}