using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class ContactMapper: BaseMapper<BLL.App.DTO.Contact, DAL.App.DTO.Contact>, IBaseMapper<BLL.App.DTO.Contact, DAL.App.DTO.Contact>

    {
        public ContactMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}