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

        IContractService Contracts { get; }
        IContractCourseService ContractCourses { get; } 
        ICourseService Courses { get; } 
        ICourseRequirementService CourseRequirements { get; } 
        IDrivingSchoolService DrivingSchools { get; } 
        ILessonService Lessons { get; } 
        ILessonParticipantService LessonParticipants { get; }
        IRequirementService Requirements { get; } 
        IStatusService Statuses { get; }
        
        IBaseEntityService<BLLAppDTO.Title, DALAppDTO.Title> Titles { get; }

    }
}