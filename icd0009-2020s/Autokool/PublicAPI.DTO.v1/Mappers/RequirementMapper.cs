using System;
using System.Collections.Generic;
using AutoMapper;
using BLL.App.DTO;
using Contracts.DAL.Base.Mappers;


namespace PublicAPI.DTO.v1.Mappers
{
    public class RequirementMapper : BaseMapper<PublicAPI.DTO.v1.Entities.Requirement, BLL.App.DTO.Requirement>
    {

        public RequirementMapper(IMapper mapper) : base(mapper)
        {
        }
        
        public new PublicAPI.DTO.v1.Entities.Requirement? Map(BLL.App.DTO.Requirement? inObject)
        {
            if (inObject == null) return null;
            var outObject = new PublicAPI.DTO.v1.Entities.Requirement
            {
                Id = inObject.Id,
                Amount = inObject.Amount,
                Description = inObject.Description,
                Name = inObject.Name,
                Price = inObject.Price
            };
            return outObject;
        }

        public BLL.App.DTO.Requirement? Map(PublicAPI.DTO.v1.Entities.Requirement? inObject, BLL.App.DTO.Requirement? originalObject = null)
        {
            if (inObject == null) return null;
            
            var outObject = new BLL.App.DTO.Requirement
            {
                Id = inObject.Id,
                Amount = inObject.Amount,
                Description = inObject.Description,
                DescriptionId = originalObject?.DescriptionId ?? Guid.Empty,
                Name = inObject.Name,
                NameId = originalObject?.NameId ?? Guid.Empty,
                Price = inObject.Price,
                CourseRequirements = originalObject?.CourseRequirements ?? new List<CourseRequirement>()
            };

            return outObject;
        }
    }
}