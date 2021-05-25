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
    public class ContractCourseService: BaseEntityService<IAppUnitOfWork, IContractCourseRepository, BLLAppDTO.ContractCourse, DALAppDTO.ContractCourse>, IContractCourseService
    {
        public ContractCourseService(IAppUnitOfWork serviceUow, IContractCourseRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new ContractCourseMapper(mapper))
        {
        }
        

        public async Task<IEnumerable<BLLAppDTO.ContractCourse>> GetByContract(Guid contractId, bool noTracking = true)
        {
            return (await ServiceRepository.GetByContract(contractId, noTracking)).Select(x => Mapper.Map(x))!;
        }

        public async Task<BLLAppDTO.ContractCourse> GetByContract(Guid contractId, Guid courseId, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.GetByContract(contractId, courseId, noTracking))!;
        }

        public async Task<double> GetDrivingLessonHours(Guid contractCourseId, bool noTracking = true)
        {
            return await ServiceRepository.GetDrivingLessonHours(contractCourseId, noTracking);
        }
    }
}