using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App;
using BLL.App.DTO;
using Contracts.BLL.App;
using Contracts.DAL.App;
using DAL.App.EF;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace TestProject.UnitTests
{
    public class ContractServiceTests
    {
        private readonly IAppBLL _bll;
        private readonly AppDbContext _ctx;
        private readonly IAppUnitOfWork _uow;
        private readonly ITestOutputHelper _testOutputHelper;

        public ContractServiceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            // set up db context for testing - using InMemory db engine
            var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            // provide new random database name here
            // or parallel test methods will conflict each other
            optionBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _ctx = new AppDbContext(optionBuilder.Options);
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();

            var mockUowAutoMapper =
                new Mapper(new MapperConfiguration(mc =>
                        mc.AddProfile(new DAL.App.DTO.MappingProfiles.AutoMapperProfile())).CreateMapper()
                    .ConfigurationProvider);
            var mockBllAutoMapper =
                new Mapper(new MapperConfiguration(mc =>
                        mc.AddProfile(new BLL.App.DTO.MappingProfiles.AutoMapperProfile())).CreateMapper()
                    .ConfigurationProvider);
            _uow = new AppUnitOfWork(_ctx, mockUowAutoMapper);
            _bll = new AppBLL(_uow, mockBllAutoMapper);

            
        }

        [Fact]
        public async Task Action_Test__Get_All_Empty()
        {
            // ACT
            var contracts = await _bll.Contracts.GetAllAsync();
            // ASSERT
            Assert.Empty(contracts);
        }

        [Fact]
        public async Task? Action_Test__Single_Search()
        {
            // ACT
            await SeedContracts(1);
            var owner = await _ctx.Users.FirstOrDefaultAsync(user => user.UserName == "Owner");
            var drivingSchool = await _ctx.DrivingSchools.FirstOrDefaultAsync(school => school.AppUserId == owner!.Id);
            var student = await _bll.Contracts.GetContractByUsername("Student0", drivingSchool.Id, Helpers.Seeding.Constants.Titles.Student, Helpers.Seeding.Constants.Statuses.Active);            Assert.NotNull(student);
            var titleRequest = await _bll.Contracts.FirstOrDefaultAsync(student!.Id);
            // ASSERT
            Assert.NotNull(titleRequest);
        }
        
        [Fact]
        public async Task Action_Test__Get_Contracts_Search()
        {
            // ACT
            await SeedContracts(2);
            var owner = await _ctx.Users.FirstOrDefaultAsync(user => user.UserName == "Owner");
            var drivingSchool = await _ctx.DrivingSchools.FirstOrDefaultAsync(school => school.AppUserId == owner!.Id);
            var students = await _bll.Contracts.GetSchoolContractsByUsername("Student", drivingSchool.Id);
            students.Count().Should().Be(2);
            var students2 = await _bll.Contracts.GetSchoolContractsByName("k S", drivingSchool.Id);
            students2.Count().Should().Be(2);
        }


        [Fact]
        public async Task Action_Test__Create_New_Single()
        {
            // ACT
            _bll.Titles.Add(
                new BLL.App.DTO.Title()
            );
            await _bll.SaveChangesAsync();
            // ASSERT
            var titles = await _bll.Titles.GetAllAsync();
            Assert.Single(titles);
        }


        [Fact]
        public async Task Action_Test__Create_New_Multiple()
        {
            // ARRANGE
            await SeedContracts(2);
            // ASSERT
            var titles = await _bll.Contracts.GetAllAsync();
            titles
                .Should().NotBeNull()
                .And.HaveCount(2);
        }
        

        [Fact]
        public async Task Action_Test__Exists()
        {
            // ACT
            var contract = new BLL.App.DTO.Contract()
            {
            };
            _bll.Contracts.Add(
                contract
            );
            await _bll.SaveChangesAsync();
            contract = _bll.Contracts.GetUpdatedEntityAfterSaveChanges(contract);
            var exists = await _bll.Contracts.ExistsAsync(contract.Id);
            // ASSERT
            exists.Should().BeTrue();
        }
        
        [Fact]
        public async Task? Action_Test__Is_Contract_From_School_With_Title()
        {
            // ACT
            await SeedContracts(1);
            var owner = await _ctx.Users.FirstOrDefaultAsync(user => user.UserName == "Owner");
            var drivingSchool = await _ctx.DrivingSchools.FirstOrDefaultAsync(school => school.AppUserId == owner!.Id);
            var student = await _bll.Contracts.GetContractByUsername("Student0", drivingSchool.Id, Helpers.Seeding.Constants.Titles.Student, Helpers.Seeding.Constants.Statuses.Active);
            var confirmation = await _bll.DrivingSchools.IsContractInSchoolWithTitle(student!.Id, drivingSchool.Id, Helpers.Seeding.Constants.Titles.Student);
            // ASSERT
            confirmation.Should().BeTrue();
        }
        
        [Fact]
        public async Task? Action_Test__Get_Contractor_Name()
        {
            // ACT
            await SeedContracts(1);
            var owner = await _ctx.Users.FirstOrDefaultAsync(user => user.UserName == "Owner");
            var drivingSchool = await _ctx.DrivingSchools.FirstOrDefaultAsync(school => school.AppUserId == owner!.Id);
            var student = await _bll.Contracts.GetContractByUsername("Student0", drivingSchool.Id, Helpers.Seeding.Constants.Titles.Student, Helpers.Seeding.Constants.Statuses.Active);
            var studentName = await _bll.Contracts.GetContractorName(student!.Id);
            // ASSERT
            studentName.Should().BeEquivalentTo("Mark Surk");
        }
        
        [Fact]
        public async Task? Action_Test__Invite_User_To_School()
        {
            // ACT
            await SeedContracts(1);
            var user = _ctx.CreateUser("User", "Roofie", "Toofie", "emas@em.com", "salt ausjhdfjkhesaghbdfjsg");
            var owner = await _ctx.Users.FirstOrDefaultAsync(user => user.UserName == "Owner");
            var drivingSchool = await _ctx.DrivingSchools.FirstOrDefaultAsync(school => school.AppUserId == owner!.Id);
            var confirmation = await _bll.DrivingSchools.InviteUserToSchool(drivingSchool.Id, "User", Helpers.Seeding.Constants.Titles.Student);
            // ASSERT
            confirmation.Should().BeTrue();
        }
        
        [Fact]
        public async Task? Action_Test__Get_School_Contracts()
        {
            // ACT
            await SeedContracts(3);
            var owner = await _ctx.Users.FirstOrDefaultAsync(user => user.UserName == "Owner");
            var drivingSchool = await _ctx.DrivingSchools.FirstOrDefaultAsync(school => school.AppUserId == owner!.Id);
            var students = await _bll.Contracts.GetContractsBySchool(drivingSchool!.Id, Helpers.Seeding.Constants.Titles.Student, Helpers.Seeding.Constants.Statuses.Active);
            // ASSERT
            students.Count().Should().Be(3);
        }
        
        [Fact]
        public async Task? Action_Test__Get_Teacher()
        {
            // ACT
            await SeedContracts(1, 1);
            var owner = await _ctx.Users.FirstOrDefaultAsync(user => user.UserName == "Owner");
            var drivingSchool = await _ctx.DrivingSchools.FirstOrDefaultAsync(school => school.AppUserId == owner!.Id);
            var teacher = await _bll.Contracts.GetContractByUsername("Teacher0", drivingSchool.Id, Helpers.Seeding.Constants.Titles.Teacher, Helpers.Seeding.Constants.Statuses.Active);
            var teacherDTO = await _bll.Contracts.GetTeacher(teacher!.Id);
            var expectedTeacherDTO = new BLL.App.DTO.Teacher()
            {
                ContractCourses = teacher.ContractCourses!.ToList(),
                ContractId = teacher.Id,
                Courses = new List<Course>(),
                CoursesNameRep = "",
                Email = teacher.AppUser!.Email,
                Name = teacher.AppUser.FullName
            };
            // ASSERT
            teacherDTO.Should().NotBeNull();
            teacherDTO.Should().BeEquivalentTo(expectedTeacherDTO);
        }
        private async Task SeedContracts(int studentCount = 0, int teacherCount = 0)
        {
            var ownerTitle = _ctx.Titles.Add(new Domain.App.Title
            {
                Name = Helpers.Seeding.Constants.Titles.Owner
            });
            var studentTitle = _ctx.Titles.Add(new Domain.App.Title
            {
                Name = Helpers.Seeding.Constants.Titles.Student
            });
            var teacherTitle = _ctx.Titles.Add(new Domain.App.Title
            {
                Name = Helpers.Seeding.Constants.Titles.Teacher
            });
            var activeStatus = _ctx.Statuses.Add(new Domain.App.Status
            {
                Name = Helpers.Seeding.Constants.Statuses.Active
            });
            var inactiveStatus = _ctx.Statuses.Add(new Domain.App.Status
            {
                Name = Helpers.Seeding.Constants.Statuses.Inactive
            });
            await _ctx.SaveChangesAsync();
            var owner = _ctx.CreateUser("Owner", "Schoolies", "Owneronies", "em@em.com", "salt ausjhdfjkhesaghbdfjsg");
            await _ctx.SaveChangesAsync();
            var drivingSchool = _ctx.DrivingSchools.Add(new Domain.App.DrivingSchool
            {
                AppUserId = owner.Id,
                Description = "Muhvin",
                Name = "Kool"
            });
            await _ctx.SaveChangesAsync();
            for (int i = 0; i < studentCount; i++)
            {
                var student = _ctx.CreateUser($"Student{i}", "Mark", "Surk", $"em{i}@em.com", "salt ausjhdfjkhesaghbdfjsg");
                var contract = _ctx.Contracts.Add(
                    new Domain.App.Contract()
                    {
                        AppUserId = student.Id,
                        DrivingSchoolId = drivingSchool.Entity.Id,
                        StatusId = activeStatus.Entity.Id,
                        TitleId = studentTitle.Entity.Id,
                    }
                );
                await _ctx.SaveChangesAsync();

                contract.State = EntityState.Detached;
            }
            for (int i = 0; i < teacherCount; i++)
            {
                var student = _ctx.CreateUser($"Teacher{i}", "Surk", "Mark", $"ema{i}@em.com", "salt ausjhdfjkhesaghbdfjsg");
                var contract = _ctx.Contracts.Add(
                    new Domain.App.Contract()
                    {
                        AppUserId = student.Id,
                        DrivingSchoolId = drivingSchool.Entity.Id,
                        StatusId = activeStatus.Entity.Id,
                        TitleId = teacherTitle.Entity.Id,
                    }
                );
                await _ctx.SaveChangesAsync();

                contract.State = EntityState.Detached;
            }

            await _ctx.SaveChangesAsync();
            drivingSchool.State = EntityState.Detached;
            ownerTitle.State = EntityState.Detached;
            studentTitle.State = EntityState.Detached;
            teacherTitle.State = EntityState.Detached;
            activeStatus.State = EntityState.Detached;
            inactiveStatus.State = EntityState.Detached;

        }
        
    }
}