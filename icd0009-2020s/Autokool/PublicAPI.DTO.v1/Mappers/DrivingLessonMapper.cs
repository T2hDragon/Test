using System;
using System.Collections.Generic;
using AutoMapper;
using BLL.App.DTO;
using Contracts.DAL.Base.Mappers;


namespace PublicAPI.DTO.v1.Mappers
{
    public class DrivingLessonMapper : BaseMapper<PublicAPI.DTO.v1.Entities.DrivingLesson, BLL.App.DTO.DrivingLesson>
    {
        public DrivingLessonMapper(IMapper mapper) : base(mapper)
        {
        }

        public new PublicAPI.DTO.v1.Entities.DrivingLesson? Map(BLL.App.DTO.DrivingLesson? inObject)
        {
            if (inObject == null) return null;
            var outObject = new PublicAPI.DTO.v1.Entities.DrivingLesson
            {
                Id = inObject.LessonId,
                Teachers = inObject.Teachers,
                Students = inObject.Students,
                CourseName = inObject.CourseName,
                Start = inObject.Start,
                End = inObject.End,
            };
            return outObject;
        }

        public new BLL.App.DTO.DrivingLesson? Map(PublicAPI.DTO.v1.Entities.DrivingLesson? inObject)
        {
            if (inObject == null) return null;

            var outObject = new BLL.App.DTO.DrivingLesson
            {
                LessonId = inObject.Id,
                Teachers = inObject.Teachers,
                Students = inObject.Students,
                CourseName = inObject.CourseName,
                Start = inObject.Start,
                End = inObject.End,
            };

            return outObject;
        }
    }
}