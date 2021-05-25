using System;
using System.Collections.Generic;
using AutoMapper;
using BLL.App.DTO;
using Contracts.DAL.Base.Mappers;


namespace PublicAPI.DTO.v1.Mappers
{
    public class TitleMapper : BaseMapper<PublicAPI.DTO.v1.Entities.Title, BLL.App.DTO.Title>
    {

        public TitleMapper(IMapper mapper) : base(mapper)
        {
        }
        
        public new PublicAPI.DTO.v1.Entities.Title? Map(BLL.App.DTO.Title? inObject)
        {
            if (inObject == null) return null;
            var outObject = new PublicAPI.DTO.v1.Entities.Title
            {
                Id = inObject.Id,
                Name = inObject.Name,
            };
            return outObject;
        }

        public BLL.App.DTO.Title? Map(PublicAPI.DTO.v1.Entities.Title? inObject, BLL.App.DTO.Title? originalObject = null)
        {
            if (inObject == null) return null;
            
            var outObject = new BLL.App.DTO.Title
            {
                Id = inObject.Id,
                Name = inObject.Name,
                Contracts = originalObject?.Contracts ?? new List<Contract>()
            };

            return outObject;
        }
    }
}