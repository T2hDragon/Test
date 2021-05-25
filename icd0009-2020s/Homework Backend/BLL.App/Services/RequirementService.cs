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
    public class RequirementService: BaseEntityService<IAppUnitOfWork, IRequirementRepository, BLLAppDTO.Requirement, DALAppDTO.Requirement>, IRequirementService
    {
        public RequirementService(IAppUnitOfWork serviceUow, IRequirementRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new RequirementMapper(mapper))
        {
        }
    }
}