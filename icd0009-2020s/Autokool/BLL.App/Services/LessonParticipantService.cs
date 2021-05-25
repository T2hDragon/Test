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
    public class LessonParticipantService: BaseEntityService<IAppUnitOfWork, ILessonParticipantRepository, BLLAppDTO.LessonParticipant, DALAppDTO.LessonParticipant>, ILessonParticipantService
    {
        public LessonParticipantService(IAppUnitOfWork serviceUow, ILessonParticipantRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new LessonParticipantMapper(mapper))
        {
        }

        public async Task<IEnumerable<BLLAppDTO.LessonParticipant>> GetContractLessonPartitions(Guid contractId, DateTime? searchStart,
            DateTime? searchEnd, bool noTracking = true)
        {
            return (await ServiceRepository.GetContractLessonPartitions(contractId, searchStart, searchEnd, noTracking)).Select(
                x => Mapper.Map(x))!;
        }

        public async Task<IEnumerable<BLLAppDTO.LessonParticipant>> GetContractDrivingLessonPartitions(Guid contractId, DateTime? searchStart = null, DateTime? searchEnd = null,
            bool noTracking = true)
        {
            return (await ServiceRepository.GetContractDrivingLessonPartitions(contractId, searchStart, searchEnd, noTracking)).Select(
                x => Mapper.Map(x))!;        
        }

        public async Task<IEnumerable<BLLAppDTO.LessonParticipant>> GetContractCourseDrivingLessonPartitions(Guid contractCourseId, DateTime? searchStart = null,
            DateTime? searchEnd = null, bool noTracking = true)
        {
            return (await ServiceRepository.GetContractCourseDrivingLessonPartitions(contractCourseId, searchStart, searchEnd, noTracking)).Select(
                x => Mapper.Map(x))!;         }
    }
}