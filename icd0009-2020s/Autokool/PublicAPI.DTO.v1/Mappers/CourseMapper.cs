using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace PublicAPI.DTO.v1.Mappers
{
    public class CourseMapper : BaseMapper<PublicAPI.DTO.v1.Entities.Course, BLL.App.DTO.Course>
    {
        public CourseMapper(IMapper mapper) : base(mapper)
        {
        }

        public new PublicAPI.DTO.v1.Entities.Course? Map(BLL.App.DTO.Course? inObject)
        {
            if (inObject == null) return null;
            var outObject = new PublicAPI.DTO.v1.Entities.Course
            {
                Id = inObject.Id,
                Price = inObject.Price,
                Description = inObject.Description!,
                Name = inObject.Name,
                Category = inObject.Category,
            };
            return outObject;
        }

        public BLL.App.DTO.Course? Map(PublicAPI.DTO.v1.Entities.Course? inObject,
            BLL.App.DTO.Course originalObject)
        {
            if (inObject == null) return null;

            var outObject = new BLL.App.DTO.Course
            {
                Id = inObject.Id,
                Price = inObject.Price,
                DescriptionId = originalObject.DescriptionId,
                Description = inObject.Description,
                Name = inObject.Name,
                NameId = originalObject.NameId,
                Category = inObject.Category,
                DrivingSchoolId = originalObject.DrivingSchoolId,
                DrivingSchool = originalObject.DrivingSchool,
                ContractCourses = originalObject.ContractCourses,
                CourseRequirements = originalObject.CourseRequirements
            };

            return outObject;
        }
    }
}