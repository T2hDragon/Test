using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mapper;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;
using DrivingSchool = DAL.App.DTO.DrivingSchool;

    
    
namespace DAL.App.EF.Repositories
{
    public class DrivingSchoolRepository : BaseRepository<DAL.App.DTO.DrivingSchool, Domain.App.DrivingSchool, AppDbContext>, IDrivingSchoolRepository
    {

        
        public DrivingSchoolRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DrivingSchoolMapper(mapper))
        {

        }

        public override DrivingSchool Update(DrivingSchool entity)
        {
            var domainEntity = Mapper.Map(entity);
            domainEntity!.Name = 
                RepoDbContext.LangStrings
                    .Include(t => t.Translations)
                    .First(x => x.Id == domainEntity.NameId);
            domainEntity!.Description = 
                RepoDbContext.LangStrings
                    .Include(t => t.Translations)
                    .First(x => x.Id == domainEntity.DescriptionId);
            domainEntity!.Description.SetTranslation(entity.Description);
            domainEntity!.Name.SetTranslation(entity.Name);

            
            var updatedEntity = RepoDbSet.Update(domainEntity!).Entity;
            var dalEntity = Mapper.Map(updatedEntity);
            return dalEntity!;        
        }

        public override DrivingSchool Add(DrivingSchool entity)
        {

            RepoDbContext.AddUserToRole(entity.AppUserId, "owner");
            return base.Add(entity);
        }

        public override async Task<IEnumerable<DrivingSchool>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            var resQuery = query
                .Include(p => p.AppUser)
                .Include(x => x.Name)
                .ThenInclude(t => t!.Translations)
                .Include(x => x.Description)
                .ThenInclude(t => t!.Translations)
                .OrderBy(ds => ds.Id)
                .AsSingleQuery()
                .Select(x => Mapper.Map(x));
            
            var res = await resQuery.ToListAsync();

            
            return res!;
            
        }

        public override async Task<DrivingSchool?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            var resQuery = query
                .Include(p => p.AppUser)
                .Include(ds => ds.Name)
                .ThenInclude(name => name.Translations)
                .Include(ds => ds.Description)
                .ThenInclude(name => name.Translations)
                .AsSingleQuery()
                .FirstOrDefaultAsync(ds => ds.Id == id);
            
            var res = Mapper.Map(await resQuery);
            
            return res!;

        }

        public async Task<bool> InviteUserToSchool(Guid schoolId, string username, string title)
        {
            var user = await RepoDbContext.AppUsers.FirstOrDefaultAsync(appUser => appUser.UserName == username);
            if (user == null)
            {
                return false;
            }

            var hasActiveContract = await RepoDbContext.Contracts
                .Include(c => c.Title)
                .AnyAsync(c => c.DrivingSchoolId == schoolId & c.AppUserId == user.Id & c.Title!.Name == title);

            if (hasActiveContract)
            {
                return false;
            }

            var domainStatus = await RepoDbContext.Statuses.FirstOrDefaultAsync(status =>
                status.Name == Domain.App.Constants.Statuses.Invited);
            var domainTitle = await RepoDbContext.Titles.FirstOrDefaultAsync(t =>
                t.Name == title);
            var contract = new Domain.App.Contract()
            {
                AppUserId = user.Id,
                ContractCourses = new List<Domain.App.ContractCourse>(),
                DrivingSchoolId = schoolId,
                Status = domainStatus,
                Title = domainTitle
            };
            await RepoDbContext.Contracts.AddAsync(contract);
            var success = await RepoDbContext.SaveChangesAsync() > 0;

            return success;
        }
        
        public async Task<bool> IsContractInSchoolWithTitle(Guid contractId, Guid schoolId, string title)
        {
            var query = CreateQuery(default);

            var queryRes = query
                .Include(school =>  school.Contracts)
                .ThenInclude(contract => contract.Title)
                .AnyAsync(school => school.Contracts!.Any(contract => contract.Id == contractId & contract.Title!.Name == title));
            return await queryRes;
        }

        public async Task<DrivingSchool?> GetAppUserDrivingSchool(Guid userId, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);


            var resQuery = query
                .Include(p => p.AppUser)
                .Include(ds => ds.Name)
                .ThenInclude(name => name.Translations)
                .Include(ds => ds.Description)
                .ThenInclude(name => name.Translations)
                .Include(ds => ds.Courses)
                .ThenInclude(ds => ds.Name)
                .ThenInclude(name => name.Translations)
                .Include(ds => ds.Courses)
                .ThenInclude(ds => ds.Description)
                .ThenInclude(name => name.Translations)
                .AsSingleQuery()
                .FirstOrDefaultAsync(ds => ds.AppUserId == userId);
            
            var res = Mapper.Map(await resQuery);
            
            return res;
        }

        public async Task<bool> HasUserWithTitle(string username, Guid schoolId, string title, bool noTracking = true)
        {
            var titleObj = await RepoDbContext.Titles.FirstOrDefaultAsync(t => t.Name == title);
            var query = CreateQuery(default, noTracking);
            
            var resQuery = await query
                .Include(school => school.Contracts)
                .ThenInclude(contract => contract.Title)
                .Include(school => school.Contracts)
                .ThenInclude(contract => contract.AppUser)
                .AsSingleQuery()
                .Where(school => school.Contracts!.FirstOrDefault(contract => 
                                     contract.AppUser!.UserName == username & contract.Title!.Name == title
                                 ) !=null
                                 & school.Id == schoolId
                )
                .AnyAsync();
            return resQuery;
        }
        
        public async Task<bool> HasUserWithTitle(Guid appUserId, Guid schoolId, string title, bool noTracking = true)
        {
            var titleObj = await RepoDbContext.Titles.FirstOrDefaultAsync(t => t.Name == title);
            var query = CreateQuery(default, noTracking);
            
            var resQuery = await query
                .Include(school => school.Contracts)
                .ThenInclude(contract => contract.Title)
                .Where(school => school.Contracts!.FirstOrDefault(contract => 
                                     contract.AppUserId == appUserId & contract.Title!.Name == title
                                 ) !=null
                                 & school.Id == schoolId
                )
                .AnyAsync();
            return resQuery;
        }

        public async Task<DrivingSchool?> GetDrivingSchoolByContract(Guid contractId, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);


            var resQuery = await query
                .Include(school => school.Contracts)
                .Include(x => x.Name)
                .ThenInclude(t => t!.Translations)
                .Include(ds => ds.Description)
                .ThenInclude(name => name.Translations)
                .AsSingleQuery()
                .FirstOrDefaultAsync(school => school.Contracts!.Any(contract => contract.Id == contractId));

            return Mapper.Map(resQuery);

        }

        public async Task<bool> IsOwner(Guid appUserId, Guid schoolId)
        {
            var query = CreateQuery();


            var resQuery = query
                .Include(school => school.AppUser)
                .AnyAsync(school => school.AppUserId == appUserId & school.Id == schoolId);
            return await resQuery;

        }

        public override async Task<DrivingSchool> RemoveAsync(Guid id, Guid userId = default)
        {
            var query = CreateQuery();

            if (userId == default)
            {
                return Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id == Guid.Empty))!;
            }
            

            var drivingSchool = await query
                .Include(school => school.Contracts)
                .Include(school => school.Courses)
                .AsSingleQuery()
                .FirstOrDefaultAsync(school => school.Id == id);

            RepoDbContext.Contracts.RemoveRange(drivingSchool.Contracts ?? new List<Contract>());
            RepoDbContext.Courses.RemoveRange(drivingSchool.Courses ?? new List<Course>());

            return Mapper.Map(RepoDbContext.DrivingSchools.Remove(drivingSchool).Entity)!;
        }
    }
}