using System;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base;
using Contracts.BLL.Base.Services;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App
{
    public interface IAppBLL: IBaseBLL
    {
        IPersonService Persons { get; }
        
        IContactService Contacts { get; } 
        IContactTypeService ContactTypes { get; } 

        IContractService Contracts { get; }
        IContractCourseService ContractCourses { get; } 
        ICourseService Courses { get; } 
        ICourseRequirementService CourseRequirements { get; } 
        IDrivingSchoolService DrivingSchools { get; } 
        ILessonService Lessons { get; } 
        ILessonCourseRequirementService LessonCourseRequirements { get; } 
        ILessonParticipantService LessonParticipants { get; } 
        ILessonParticipantConfirmationService LessonParticipantConfirmations { get; } 
        ILessonParticipantNoteService LessonParticipantNotes { get; } 
        IRequirementService Requirements { get; } 
        IWorkHourService WorkHours { get; } 
        IBaseEntityService<BLLAppDTO.Simple, DALAppDTO.Simple> Simples { get; }
        
        IBaseEntityService<BLLAppDTO.IntPkThing, DALAppDTO.IntPkThing, int> IntPkThings { get; }

    }
}