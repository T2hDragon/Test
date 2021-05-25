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
    public class StatusService: BaseEntityService<IAppUnitOfWork, IStatusRepository, BLLAppDTO.Status, DALAppDTO.Status>, IStatusService
    {
        public StatusService(IAppUnitOfWork serviceUow, IStatusRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new StatusMapper(mapper))
        {
        }

        public async Task<BLLAppDTO.Status?> GetStatusByName(string name, bool noTracing = true)
        {
            return Mapper.Map(await ServiceRepository.GetStatusByName(name, noTracing));
        }
    }
}