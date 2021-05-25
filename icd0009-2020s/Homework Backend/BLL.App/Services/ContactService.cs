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
    public class ContactService: BaseEntityService<IAppUnitOfWork, IContactRepository, BLLAppDTO.Contact, DALAppDTO.Contact>, IContactService
    {
        public ContactService(IAppUnitOfWork serviceUow, IContactRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new ContactMapper(mapper))
        {
        }
    }
}