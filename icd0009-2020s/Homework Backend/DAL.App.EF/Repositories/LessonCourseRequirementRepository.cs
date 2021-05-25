
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mapper;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class LessonCourseRequirementRepository : BaseRepository<DAL.App.DTO.LessonCourseRequirement, Domain.App.LessonCourseRequirement, AppDbContext>, ILessonCourseRequirementRepository
    {
        public LessonCourseRequirementRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new LessonCourseRequirementMapper(mapper))
        {
        }
    }
}