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
    public class ContactTypeService: BaseEntityService<IAppUnitOfWork, IContactTypeRepository, BLLAppDTO.ContactType, DALAppDTO.ContactType>,  IContactTypeService
    {
        public ContactTypeService(IAppUnitOfWork serviceUow, IContactTypeRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new ContactTypeMapper(mapper))
        {
        }

        public async Task<IEnumerable<BLLAppDTO.ContactType>> GetAllWithContactCountAsync(bool noTracking = true)
        {
            return (await ServiceRepository.GetAllWithContactCountAsync(noTracking)).Select(x => Mapper.Map(x))!;
        }

        public async Task<int> GetPersonUniqueContactTypeCounts(Guid personId)
        {
            return await ServiceRepository.GetPersonUniqueContactTypeCounts(personId);
        }
    }
}