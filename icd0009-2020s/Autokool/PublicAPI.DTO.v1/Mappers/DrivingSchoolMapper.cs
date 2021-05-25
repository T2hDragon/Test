using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace PublicAPI.DTO.v1.Mappers
{
    public class DrivingSchoolMapper : BaseMapper<PublicAPI.DTO.v1.Entities.DrivingSchool, BLL.App.DTO.DrivingSchool>
    {
        public DrivingSchoolMapper(IMapper mapper) : base(mapper)
        {
        }

        public new PublicAPI.DTO.v1.Entities.DrivingSchool? Map(BLL.App.DTO.DrivingSchool? inObject)
        {
            if (inObject == null) return null;
            var outObject = new PublicAPI.DTO.v1.Entities.DrivingSchool
            {
                Id = inObject.Id,
                Name = inObject.Name,
                Description = inObject.Description

            };
            return outObject;
        }

    }

}