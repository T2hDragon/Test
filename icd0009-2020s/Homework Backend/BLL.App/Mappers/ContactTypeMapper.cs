using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class ContactTypeMapper: BaseMapper<BLL.App.DTO.ContactType, DAL.App.DTO.ContactType>, IBaseMapper<BLL.App.DTO.ContactType, DAL.App.DTO.ContactType>
    {
        public ContactTypeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}