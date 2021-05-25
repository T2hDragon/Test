using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class DrivingSchoolMapper: BaseMapper<BLL.App.DTO.DrivingSchool, DAL.App.DTO.DrivingSchool>, IBaseMapper<BLL.App.DTO.DrivingSchool, DAL.App.DTO.DrivingSchool>
    {
        public DrivingSchoolMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}