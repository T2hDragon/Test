using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace PublicAPI.DTO.v1.Mappers
{
    public class ContractMapper : BaseMapper<PublicAPI.DTO.v1.Entities.Contract, BLL.App.DTO.Contract>
    {
        public ContractMapper(IMapper mapper) : base(mapper)
        {
        }

        public new PublicAPI.DTO.v1.Entities.Contract? Map(BLL.App.DTO.Contract? inObject)
        {
            if (inObject == null) return null;
            var outObject = new PublicAPI.DTO.v1.Entities.Contract
            {
                ContractId = inObject.Id,
                Name = inObject.AppUser!.FullName,
                DrivingSchoolId = inObject.DrivingSchoolId,
                DrivingSchoolName = inObject.DrivingSchool!.Name,
                Title = inObject.Title!.Name,
                Status = inObject.Status!.Name
            };
            return outObject;
        }

    }

}