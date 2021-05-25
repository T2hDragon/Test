using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mapper;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;
using Contract = DAL.App.DTO.Contract;
using ContractCourse = DAL.App.DTO.ContractCourse;
using LessonParticipant = DAL.App.DTO.LessonParticipant;


namespace DAL.App.EF.Repositories
{
    public class ContractRepository : BaseRepository<DAL.App.DTO.Contract, Domain.App.Contract, AppDbContext>,
        IContractRepository
    {
        public ContractRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ContractMapper(mapper))
        {
        }

        public override async Task<IEnumerable<Contract>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery();

            var queryResult = await query
                .Include(contract => contract.DrivingSchool)
                .Include(contract => contract.Title)
                .Include(contract => contract.Status)
                .ToListAsync();
            return queryResult.Select(x => Mapper.Map(x))!;
        }

        public override async Task<Contract?> FirstOrDefaultAsync(Guid id, Guid userId = default,
            bool noTracking = true)
        {
            var query = CreateQuery();

            var queryResult = await query
                .Include(contract => contract.DrivingSchool)
                .Include(contract => contract.Title)
                .Include(contract => contract.Status)
                .Include(contract => contract.AppUser)
                .AsSingleQuery()
                .FirstOrDefaultAsync(contract => contract!.Id == id);

            return Mapper.Map(queryResult);
        }

        public async Task<Contract?> FirstOrDefaultWithCoursesAsync(Guid id, Guid userId = default,
            bool noTracking = true)
        {
            var query = CreateQuery();

            var queryResult = await query
                .Include(contract => contract.AppUser)
                .Include(contract => contract.ContractCourses)
                .ThenInclude(cc => cc.Course)
                .ThenInclude(course => course!.Name)
                .ThenInclude(s => s.Translations)
                .Include(contract => contract.Title)
                .AsSingleQuery()
                .FirstOrDefaultAsync(x => x!.Id == id);

            return Mapper.Map(queryResult);
        }


        public async Task<IEnumerable<Contract?>> GetContractsBySchool(Guid schoolId, string? title = null, string? status = null,
            bool noTracking = true)
        {
            var query = CreateQuery();

            var queryResult = await query
                .Include(contract => contract.AppUser)
                .Include(contract => contract.Title)
                .Include(contract => contract.Status)
                .Include(contract => contract.ContractCourses)
                .ThenInclude(contractCourse => contractCourse.Course)
                .ThenInclude(course => course!.Name)
                .ThenInclude(s => s.Translations)
                .OrderBy(contract => contract.Id)
                .AsSingleQuery()
                .Where(contract => contract.DrivingSchoolId == schoolId & contract.Title!.Name == title
                                   & (contract.Status!.Name == status | status == null)
                                   & (contract.Title!.Name == title | title == null))
                .ToListAsync();
            return queryResult.Select(x => Mapper.Map(x!));
        }



        public async Task<string> GetContractorName(Guid contractId, bool noTracing = true)
        {
            var query = CreateQuery(default, noTracing);

            var queryRes = query
                .Include(contract => contract.AppUser)
                .Where(contract => contract.Id == contractId)
                .FirstOrDefaultAsync(contract => contract.Id == contractId);
            return (await queryRes).AppUser!.FullName;
        }


        public async Task<Contract?> GetContractByUsername(string username, Guid schoolId, string title, string status)
        {
            var query = CreateQuery();

            var queryRes = await query
                .Include(contract => contract.AppUser)
                .Include(contract => contract.Title)
                .Include(contract => contract.Status)
                .FirstOrDefaultAsync(contract => contract.AppUser!.UserName == username
                    & contract.Title!.Name == title
                    & contract.Status!.Name == status
                    & contract.DrivingSchoolId == schoolId);

            return Mapper.Map(queryRes);
        }

        public async Task<IEnumerable<Contract>> GetSchoolContractsByUsername(string username, Guid schoolId, string? title = null, string? status = null)
        {
            var query = CreateQuery();

            var queryRes = await query
                .Include(contract => contract.AppUser)
                .Include(contract => contract.Title)
                .Include(contract => contract.Status)
                .Where(contract => contract.AppUser!.UserName.ToUpper().Contains(username.Trim().ToUpper())
                    & (contract.Title!.Name == title | title == null)
                    & (contract.Status!.Name == status | status == null)
                    & contract.DrivingSchoolId == schoolId)
                .ToListAsync();

            return queryRes.Select(x => Mapper.Map(x))!;
        }


        public async Task<IEnumerable<Contract>> GetSchoolContracts(Guid schoolId, string username = "", string fullname = "", string? title = null,
            string? status = null)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));
            var query = CreateQuery();

            var queryRes = await query
                .Include(contract => contract.AppUser)
                .Include(contract => contract.Title)
                .Include(contract => contract.Status)
                .Where(contract => 
                    contract.AppUser!.UserName.ToUpper().Contains(username.Trim().ToUpper())
                                    & (contract.AppUser!.Firstname + " " + contract.AppUser!.Lastname).ToUpper().Contains(fullname.Trim().ToUpper())
                                   & (contract.Title!.Name == title | title == null)
                                   & (contract.Status!.Name == status | status == null)
                                   & contract.DrivingSchoolId == schoolId)
                .ToListAsync();

            return queryRes.Select(x => Mapper.Map(x))!;        }

        public async Task<IEnumerable<Contract>> GetContractsByAppUser(Guid appUserId)
        {
            var query = CreateQuery();

            var queryResult = await query
                .Include(contract => contract.DrivingSchool)
                .ThenInclude(ds => ds!.Name)
                .ThenInclude(name => name.Translations)
                .Include(contract => contract.Title)
                .Include(contract => contract.Status)
                .Include(contract => contract.AppUser)
                .AsSingleQuery()
                .Where(contract => contract!.AppUserId == appUserId)
                .ToListAsync();

            return queryResult.Select(x => Mapper.Map(x))!;      
        }

        public async Task<IEnumerable<Contract>> GetSchoolContractsByName(string name, Guid schoolId, string? title = null, string? status = null)
        {
            var query = CreateQuery();

            var queryRes = await query
                .Include(contract => contract.AppUser)
                .Include(contract => contract.Title)
                .Include(contract => contract.Status)
                .Where(contract => (contract.AppUser!.Firstname + " " + contract.AppUser!.Lastname).ToUpper()
                                   .Contains(name.Trim().ToUpper())
                                   & (contract.Title!.Name == title | title == null)
                                   & (contract.Status!.Name == status | status == null)
                                   & contract.DrivingSchoolId == schoolId)
                .ToListAsync();

            return queryRes.Select(x => Mapper.Map(x))!;
        }
        


        public async Task<IEnumerable<Contract>> GetLessonContractsByTitle(Guid lessonId, string title)
        {
            var query = CreateQuery(default);

            var queryRes = await query
                .Include(contract => contract.AppUser)
                .Include(contract => contract.ContractCourses)
                .ThenInclude(contractCourse => contractCourse.LessonParticipants)
                .Include(contract => contract.Title)
                .Where(contract =>
                    contract.ContractCourses!.Any(contractCourse =>
                        contractCourse.LessonParticipants!.Any(p => p.LessonId == lessonId)) &
                    contract!.Title!.Name == title)
                .AsSingleQuery()
                .ToListAsync();

            return queryRes.Select(contract => Mapper.Map(contract))!;
        }

        public async Task<bool> IsFree(Guid contractId, DateTime startingDate, DateTime endingDate)
        {
            var contract = await RepoDbContext.Contracts.FirstOrDefaultAsync(contract => contract.Id == contractId);
            var query = CreateQuery(default);

            var queryRes = await query
                .Include(contract => contract.ContractCourses)
                .ThenInclude(contractCourse => contractCourse.LessonParticipants)
                .AnyAsync(contract => 
                    contract.AppUserId == contract.AppUserId! & 
                    contract.ContractCourses!
                    .Any(contractCourse => contractCourse.LessonParticipants!
                        .Any(participant => participant.Start < endingDate & participant.End > startingDate)
                    )
                );
            return !queryRes;
        }

        public async Task<Contract?> GetSchoolContractWithTitle(Guid appUserId, Guid schoolId, string title, string status)
        {
            var query = CreateQuery();

            var queryRes = await query
                .Include(contract => contract.AppUser)
                .Include(contract => contract.Title)
                .Include(contract => contract.Status)
                .FirstOrDefaultAsync(contract => contract.AppUserId == appUserId
                                                 & (contract.Title!.Name == title)
                                                 & (contract.Status!.Name == status)
                                                 & contract.DrivingSchoolId == schoolId);
            return Mapper.Map(queryRes)!;        
        }
    }
}