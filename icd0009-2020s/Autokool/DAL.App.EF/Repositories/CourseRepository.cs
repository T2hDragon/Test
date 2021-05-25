using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mapper;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;
using Course = DAL.App.DTO.Course;
using DrivingSchool = DAL.App.DTO.DrivingSchool;

namespace DAL.App.EF.Repositories
{
    public class CourseRepository : BaseRepository<DAL.App.DTO.Course, Domain.App.Course, AppDbContext>,
        ICourseRepository
    {
        public CourseRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new CourseMapper(mapper))
        {
        }

        public override async Task<IEnumerable<Course>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            var resQuery = query
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

        public override Course Update(Course entity)
        {
            var domainEntity = Mapper.Map(entity);
            var test = domainEntity!.Name;
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

        public override async Task<Course?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            var resQuery = query
                .Include(ds => ds.Name)
                .ThenInclude(name => name.Translations)
                .Include(ds => ds.Description)
                .ThenInclude(name => name.Translations)
                .AsSingleQuery()
                .FirstOrDefaultAsync(ds => ds.Id == id);
            
            var res = Mapper.Map(await resQuery);
            
            return res!;        }

        public async Task<IEnumerable<Course?>> GetOwnerDrivingSchoolCourses(Guid appUserId, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var queryRes = query
                .Include(course => course.DrivingSchool)
                .ThenInclude(ds => ds!.AppUser)
                .Include(ds => ds.Name)
                .ThenInclude(name => name.Translations)
                .Include(ds => ds.Description)
                .ThenInclude(name => name.Translations)
                .OrderBy(course => course.Id)
                .Where(c => c.DrivingSchool!.AppUserId == appUserId)
                .AsSingleQuery()
                .Select(x => Mapper.Map(x));
            return await queryRes.ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetSchoolCourses(Guid schoolId, bool noTracing = true)
        {
            var query = CreateQuery(default, noTracing);
            
            var queryRes = query
                .Include(course => course.DrivingSchool)
                .Include(ds => ds.Name)
                .ThenInclude(name => name.Translations)
                .Include(ds => ds.Description)
                .ThenInclude(name => name.Translations)
                .Where(c => c.DrivingSchoolId == schoolId)
                .OrderBy(course => course.Id)
                .AsSingleQuery()
                .Select(x => Mapper.Map(x));
            return (await queryRes.ToListAsync())!;
        }

        public async Task<IEnumerable<Course>> GetContractMissingCourses(Guid contractId, bool noTracing = true)
        {
            var query = CreateQuery(default, noTracing);

            var queryRes = await query
                .Include(course => course.ContractCourses)
                .Include(ds => ds.Name)
                .ThenInclude(name => name.Translations)
                .Include(ds => ds.Description)
                .ThenInclude(name => name.Translations)
                .Where(course => course.ContractCourses!.All(contractCourse => contractCourse.ContractId != contractId))
                .OrderBy(course => course.Id)
                .AsSingleQuery()
                .ToListAsync();

            return queryRes.Select(x => Mapper.Map(x))!;
        }
        

        public override async Task<Course> RemoveAsync(Guid id, Guid userId = default)
        {
            var query = CreateQuery();

            var course = await query
                .Include(school => school.ContractCourses)
                .FirstOrDefaultAsync(school => school.Id == id);

            RepoDbContext.ContractCourses.RemoveRange(course.ContractCourses ?? new List<ContractCourse>());

            return Mapper.Map(RepoDbContext.Courses.Remove(course).Entity)!;
        }
        

    }
}