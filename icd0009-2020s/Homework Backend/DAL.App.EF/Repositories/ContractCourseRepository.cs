using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mapper;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class ContractCourseRepository : BaseRepository<DAL.App.DTO.ContractCourse, Domain.App.ContractCourse, AppDbContext>, IContractCourseRepository
    {
        public ContractCourseRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ContractCourseMapper(mapper))
        {
        }

    }
}