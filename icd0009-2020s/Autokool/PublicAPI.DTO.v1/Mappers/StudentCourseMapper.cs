using System;
using System.Collections.Generic;
using AutoMapper;
using BLL.App.DTO;
using Contracts.DAL.Base.Mappers;


namespace PublicAPI.DTO.v1.Mappers
{
    public class StudentCourseMapper : BaseMapper<PublicAPI.DTO.v1.Entities.StudentCourse, BLL.App.DTO.ContractCourse>
    {

        public StudentCourseMapper(IMapper mapper) : base(mapper)
        {
        }
        
        public new PublicAPI.DTO.v1.Entities.StudentCourse? Map(BLL.App.DTO.ContractCourse? inObject)
        {
            if (inObject == null) return null;
            var outObject = new PublicAPI.DTO.v1.Entities.StudentCourse
            {
                Id = inObject.Id,
                Name = inObject.Course!.Name,
                Description = inObject.Course.Description,
                Category = inObject.Course.Category,
                ContractId = inObject.ContractId!.Value,
                CourseId = inObject.CourseId!.Value
            };
            return outObject;
        }
    }
}