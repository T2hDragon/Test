using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App;
using BLL.App.DTO;
using Contracts.BLL.App;
using Contracts.DAL.App;
using DAL.App.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PublicAPI.DTO.v1.Entities;
using WebApp.ApiControllers.Entities;

namespace TestProject.UnitTests
{
    public class TitleBaseServiceTests
    {
        private readonly IAppBLL _bll;
        private readonly AppDbContext _ctx;
        private readonly IAppUnitOfWork _uow;
        private readonly ILoggerFactory _logger;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly TitlesController _titleController;


        public TitleBaseServiceTests(ITestOutputHelper testOutputHelper)
        {
            _logger = LoggerFactory.Create(builder => builder.AddConsole());
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
            
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<TitlesController>();
            
            // SUT
            _titleController = new TitlesController(_bll,mockBllAutoMapper);
        }

        [Fact]
        public async Task Action_Test__Get_All_Empty()
        {
            // ACT
            var titles = await _bll.Titles.GetAllAsync();
            // ASSERT
            Assert.Empty(titles);
        }

        [Fact]
        public async Task Action_Test__First_Or_Default()
        {
            // ACT
            var title = new BLL.App.DTO.Title
            {
                Name = "test name"
            };
            _bll.Titles.Add(
                title
            );
            await _bll.SaveChangesAsync();
            title = _bll.Titles.GetUpdatedEntityAfterSaveChanges(title);
            var titleRequest = await _bll.Titles.FirstOrDefaultAsync(title.Id);
            // ASSERT
            Assert.NotNull(titleRequest);
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
            await SeedData(5);
            // ASSERT
            var titles = await _bll.Titles.GetAllAsync();
            titles
                .Should().NotBeNull()
                .And.HaveCount(5)
                .And.Contain(title => title.Name == "Title 0")
                .And.Contain(title => title.Name == "Title 4");
        }
        
        [Fact]
        public async Task Action_Test__Update_Title()
        {
            // ACT
            var title = _ctx.Titles.Add(new Domain.App.Title
            {
                Name = "Test Title"
            });
            await _ctx.SaveChangesAsync();
            title.State = EntityState.Detached;
            title.Entity.Name = "Totall new title";
            _bll.Titles.Update(new BLL.App.DTO.Title
            {
                Name = title.Entity.Name,
                Id = title.Entity.Id
            });
            await _bll.SaveChangesAsync();
            var bllTitle = await _bll.Titles.FirstOrDefaultAsync(title.Entity.Id);
            // ASSERT
            bllTitle
                .Should().NotBeNull();
            bllTitle!.Name
                .Should().Be("Totall new title");
        }
        
        
        [Fact]
        public async Task Action_Test__Remove_Title_Async()
        {
            // ACT
            var title = _ctx.Titles.Add(new Domain.App.Title
            {
                Name = "Test Title"
            });
            await _ctx.SaveChangesAsync();
            title.State = EntityState.Detached;
            await _bll.Titles.RemoveAsync(title.Entity.Id);
            await _ctx.SaveChangesAsync();
            // ACT
            var titles = await _bll.Titles.GetAllAsync();
            // ASSERT
            Assert.Empty(titles);
        }


        private async Task SeedData(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                _bll.Titles.Add(
                    new BLL.App.DTO.Title
                    {
                        Name = $"Title {i}"
                    }
                );
            }
            await _bll.SaveChangesAsync();
        }
        
        [Fact]
        public async Task Action_Test__Exists()
        {
            // ACT
            var title = new BLL.App.DTO.Title
            {
                Name = "test name"
            };
            _bll.Titles.Add(
                title
            );
            await _bll.SaveChangesAsync();
            title = _bll.Titles.GetUpdatedEntityAfterSaveChanges(title);
            var exists = await _bll.Titles.ExistsAsync(title.Id);
            // ASSERT
            Assert.True(exists);
        }
    }
}