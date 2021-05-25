using System.Collections.Generic;
using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mapper
{
    public class QuizMapper : IBaseMapper<DAL.App.DTO.Quiz, Domain.App.Quiz>
    {
        protected IMapper Mapper;
        public QuizMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
        public virtual DAL.App.DTO.Quiz? Map(Domain.App.Quiz? inObject)
        {
            return Mapper.Map<DAL.App.DTO.Quiz>(inObject);

        }

        public virtual Domain.App.Quiz? Map(DAL.App.DTO.Quiz? inObject)
        {
            return Mapper.Map<Domain.App.Quiz>(inObject);
        }
        
    }
}