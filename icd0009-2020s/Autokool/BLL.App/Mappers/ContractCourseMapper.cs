using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class ContractCourseMapper: BaseMapper<BLL.App.DTO.ContractCourse, DAL.App.DTO.ContractCourse>, IBaseMapper<BLL.App.DTO.ContractCourse, DAL.App.DTO.ContractCourse>
    {
        public ContractCourseMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}