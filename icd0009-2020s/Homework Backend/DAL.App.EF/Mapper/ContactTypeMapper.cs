using System;
using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mapper
{
    public class ContactTypeMapper : IBaseMapper<DAL.App.DTO.ContactType, Domain.App.ContactType>
    {
        protected IMapper Mapper;
        public ContactTypeMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
        public virtual DAL.App.DTO.ContactType? Map(Domain.App.ContactType? inObject)
        {
            if (inObject == null) return Mapper.Map<DAL.App.DTO.ContactType>(inObject);
            var DalContactType = new DAL.App.DTO.ContactType()
            {
                Id = inObject.Id,
                Type = inObject.Type,
                ContactCount = inObject.Contacts!.Count,
                Contacts = ArraySegment<Contact>.Empty
            };
            return DalContactType;
        }

        public virtual Domain.App.ContactType? Map(DAL.App.DTO.ContactType? inObject)
        {
            return Mapper.Map<Domain.App.ContactType>(inObject);
        }
    }
}