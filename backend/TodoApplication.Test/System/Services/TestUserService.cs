using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApplication.Entities;
using TodoApplication.Services;
using TodoApplication.Services.Interfaces;
using TodoApplication.Test.MockData;
using FluentAssertions;
using TodoApplication.Models.DTOs.Requests;

namespace TodoApplication.Test.System.Services
{
    public class TestUserService : IDisposable
    {
        private readonly TodoDBContext _dbContext;

        public TestUserService()
        {
            var options = new DbContextOptionsBuilder<TodoDBContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            this._dbContext = new TodoDBContext(options);

            this._dbContext.Database.EnsureCreated();
        }

        [Fact]
        public async Task login()
        {
            // Arrange
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();
            UserEntity userEntity = mapper.Map<UserEntity>(UserMockData.NewUserAuthenRequest());
            ILogger<UserService> logger = Mock.Of<ILogger<UserService>>();
            var unitOfWork = new UnitOfWork(this._dbContext);

            var sut = new UserService(unitOfWork, mapper, logger);

            // Act
            var result = await sut.register(UserMockData.NewUserAuthenRequest());
            var result1 = await sut.login(UserMockData.NewUserAuthenRequest());

            // Assert
            Assert.NotNull(result1);
            Assert.Equal(result1.Id, result.Id);
        }

        [Fact]
        public async Task register()
        {
            // Arrange
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();
            UserEntity userEntity = mapper.Map<UserEntity>(UserMockData.NewUserAuthenRequest());
            ILogger<UserService> logger = Mock.Of<ILogger<UserService>>();
            var unitOfWork = new UnitOfWork(this._dbContext);

            var sut = new UserService(unitOfWork, mapper, logger);

            // Act
            var result = await sut.register(UserMockData.NewUserAuthenRequest());
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestgetByIdAsync()
        {
            /// Arrange
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();
            UserEntity userEntity = mapper.Map<UserEntity>(UserMockData.NewUserAuthenRequest());
            ILogger<UserService> logger = Mock.Of<ILogger<UserService>>();
            var unitOfWork = new UnitOfWork(this._dbContext);

            var sut = new UserService(unitOfWork, mapper, logger);
            /// Act
            var result1 = await sut.register(UserMockData.NewUserAuthenRequest());
            UserEntity result = await sut.getByIdAsync(result1.Id);

            /// Assert
            Assert.NotNull(result);
            Assert.Equal(result1.Id, result.Id);
        }

        public void Dispose()
        {
            this._dbContext.Database.EnsureDeleted();
            this._dbContext.Dispose();
        }
    }
}
