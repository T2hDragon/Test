using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class RequirementMapper: BaseMapper<BLL.App.DTO.Requirement, DAL.App.DTO.Requirement>, IBaseMapper<BLL.App.DTO.Requirement, DAL.App.DTO.Requirement>
    {
        public RequirementMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}