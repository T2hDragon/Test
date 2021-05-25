using System;
using System.Collections.Generic;
using AutoMapper;
using BLL.App.DTO;
using Contracts.DAL.Base.Mappers;


namespace PublicAPI.DTO.v1.Mappers
{
    public class StatusMapper : BaseMapper<PublicAPI.DTO.v1.Entities.Status, BLL.App.DTO.Status>
    {

        public StatusMapper(IMapper mapper) : base(mapper)
        {
        }
        
        public new PublicAPI.DTO.v1.Entities.Status? Map(BLL.App.DTO.Status? inObject)
        {
            if (inObject == null) return null;
            var outObject = new PublicAPI.DTO.v1.Entities.Status
            {
                Id = inObject.Id,
                Name = inObject.Name,
            };
            return outObject;
        }

        public BLL.App.DTO.Status? Map(PublicAPI.DTO.v1.Entities.Status? inObject, BLL.App.DTO.Status? originalObject = null)
        {
            if (inObject == null) return null;
            
            var outObject = new BLL.App.DTO.Status
            {
                Id = inObject.Id,
                Name = inObject.Name,
                ContractCourses = originalObject?.ContractCourses ?? new List<ContractCourse>(),
                Contracts = originalObject?.Contracts ?? new List<Contract>()
            };

            return outObject;
        }
    }
}