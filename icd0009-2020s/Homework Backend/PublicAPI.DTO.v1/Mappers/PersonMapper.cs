using AutoMapper;
using Contracts.DAL.Base.Mappers;
using Microsoft.AspNetCore.Identity;

namespace PublicAPI.DTO.v1.Mappers
{
    public class PersonMapper: BaseMapper<PublicAPI.DTO.v1.Person, BLL.App.DTO.Person>, IBaseMapper<PublicAPI.DTO.v1.Person, BLL.App.DTO.Person>
    {
        public PersonMapper(IMapper mapper) : base(mapper)
        {
        }
    
        // Mapper.Map<BLL.App.DTO.Person>(personAdd);
        public static BLL.App.DTO.Person MapToBll(PersonAdd personAdd)
        {
            var result =  new BLL.App.DTO.Person()
            {
                FirstName = personAdd.FirstName.Trim().ToUpper(),
                LastName = personAdd.LastName.Trim().ToUpper(),
            };

            return result;
        }
        
    }
}