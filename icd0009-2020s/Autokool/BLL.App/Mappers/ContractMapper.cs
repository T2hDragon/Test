using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class ContractMapper: BaseMapper<BLL.App.DTO.Contract, DAL.App.DTO.Contract>, IBaseMapper<BLL.App.DTO.Contract, DAL.App.DTO.Contract>
    {
        public ContractMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}