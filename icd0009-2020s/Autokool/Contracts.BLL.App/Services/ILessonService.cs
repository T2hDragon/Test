using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface ILessonService: IBaseEntityService<BLLAppDTO.Lesson, DALAppDTO.Lesson>, ILessonRepositoryCustom<BLLAppDTO.Lesson>
    {
        public Task<BLLAppDTO.DrivingLesson?> GetDrivingLesson(Guid lessonId, bool noTracing = true);
        public Task<IEnumerable<BLLAppDTO.DrivingLesson>> GetContractDrivingLessons(Guid contractId, DateTime startingDate, DateTime endingDate, bool noTracing = true);
        public Task<IEnumerable<BLLAppDTO.DrivingLesson>> GetContractCourseDrivingLessons(Guid contractCourseId, DateTime startingDate, DateTime endingDate, bool noTracing = true);

    }
}