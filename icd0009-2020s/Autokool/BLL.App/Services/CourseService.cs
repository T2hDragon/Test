using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class CourseService: BaseEntityService<IAppUnitOfWork, ICourseRepository, BLLAppDTO.Course, DALAppDTO.Course>, ICourseService
    {
        public CourseService(IAppUnitOfWork serviceUow, ICourseRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new CourseMapper(mapper))
        {
        }

        public async Task<IEnumerable<BLLAppDTO.Course?>> GetOwnerDrivingSchoolCourses(Guid appUserId, bool noTracing = true)
        {
            var dalCourses = await ServiceRepository.GetOwnerDrivingSchoolCourses(appUserId);
            return dalCourses.Select(course => Mapper.Map(course));
        }

        public async Task<IEnumerable<BLLAppDTO.Course>> GetSchoolCourses(Guid schoolId, bool noTracing = true)
        {
            var dalCourses = await ServiceRepository.GetSchoolCourses(schoolId);

            return dalCourses.Select(course => Mapper.Map(course))!;
        }

        public async Task<IEnumerable<BLLAppDTO.Course>> GetContractMissingCourses(Guid contractId, bool noTracing = true)
        {
            return (await ServiceRepository.GetContractMissingCourses(contractId, noTracing))
                .Select(x => Mapper.Map(x))!;
        }
    }
}