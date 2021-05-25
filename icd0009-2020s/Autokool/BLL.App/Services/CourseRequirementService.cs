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
    public class CourseRequirementService: BaseEntityService<IAppUnitOfWork, ICourseRequirementRepository, BLLAppDTO.CourseRequirement, DALAppDTO.CourseRequirement>, ICourseRequirementService
    {
        public CourseRequirementService(IAppUnitOfWork serviceUow, ICourseRequirementRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new CourseRequirementMapper(mapper))
        {
        }

        public async Task<IEnumerable<BLLAppDTO.CourseRequirement>> GetAllByCourseId(Guid courseId, bool noTracing = true)
        {
            var courseRequirements = await ServiceRepository.GetAllByCourseId(courseId);
            return courseRequirements.Select(requirement => Mapper.Map(requirement))!;
        }

        public async Task<IEnumerable<BLLAppDTO.CourseRequirement>> GetAllCheckmarkableByContractCourse(Guid contractCourseId, bool hasFinished, bool noTracing = true)
        {
            return (await ServiceRepository.GetAllCheckmarkableByContractCourse(contractCourseId, hasFinished, noTracing)).Select(x => Mapper.Map(x))!;
        }

        public async Task<BLLAppDTO.CourseRequirement> CreateWithRequirementFields(Guid requirementId, Guid courseId, bool noTracing = true)
        {
            return Mapper.Map(await ServiceRepository.CreateWithRequirementFields(requirementId, courseId, noTracing))!;
        }

        public async Task<BLLAppDTO.CourseRequirement?> GetDrivingRequirement(Guid courseId, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.GetDrivingRequirement(courseId, noTracking))!;
        }
    }
}