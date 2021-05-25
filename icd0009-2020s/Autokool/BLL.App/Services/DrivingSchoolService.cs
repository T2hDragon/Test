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
    public class DrivingSchoolService: BaseEntityService<IAppUnitOfWork, IDrivingSchoolRepository, BLLAppDTO.DrivingSchool, DALAppDTO.DrivingSchool>, IDrivingSchoolService
    {
        public DrivingSchoolService(IAppUnitOfWork serviceUow, IDrivingSchoolRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new DrivingSchoolMapper(mapper))
        {
        }
        public async Task<bool> InviteUserToSchool(Guid schoolId, string username, string title)
        {
            return await ServiceRepository.InviteUserToSchool(schoolId, username, title);
        }



        public async Task<bool> IsContractInSchoolWithTitle(Guid contractId, Guid schoolId, string title)
        {
            return await ServiceRepository.IsContractInSchoolWithTitle(contractId, schoolId, title);
        }
        public async Task<bool> IsOwner(Guid userId, Guid schoolId)
        {
            return await ServiceRepository.IsOwner(userId, schoolId);
        }

        public async Task<BLLAppDTO.DrivingSchool?> GetAppUserDrivingSchool(Guid userId, bool noTracking)
        {
            return Mapper.Map(await ServiceRepository.GetAppUserDrivingSchool(userId, noTracking));
        }

        public async Task<bool> HasUserWithTitle(Guid appUser, Guid schoolId, string title, bool noTracking = true)
        {
            return await ServiceRepository.HasUserWithTitle(appUser, schoolId, title, noTracking);
        }
        
        public async Task<bool> HasUserWithTitle(string username, Guid schoolId, string title, bool noTracking = true)
        {
            return await ServiceRepository.HasUserWithTitle(username, schoolId, title, noTracking);
        }

        public async Task<BLLAppDTO.DrivingSchool?> GetDrivingSchoolByContract(Guid contractId, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.GetDrivingSchoolByContract(contractId,
                noTracking));
        }
    }
}