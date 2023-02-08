using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApplication.Constants;
using TodoApplication.Entities;
using TodoApplication.Models.DTOs;
using TodoApplication.Services;
using TodoApplication.Test.MockData;

namespace TodoApplication.Test.System.Services
{
    public class TestTaskService : IDisposable
    {
        private readonly TodoDBContext _dbContext;

        public TestTaskService()
        {
            var options = new DbContextOptionsBuilder<TodoDBContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            this._dbContext = new TodoDBContext(options);

            this._dbContext.Database.EnsureCreated();
        }

        [Fact]
        public async Task getAllByUserId_TaskStatus()
        {
            // Arrange
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();

            ILogger<TaskService> logger = Mock.Of<ILogger<TaskService>>();
            var unitOfWork = new UnitOfWork(this._dbContext);

            ILogger<UserService> loggerUser = Mock.Of<ILogger<UserService>>();
            var unitOfWorkUser = new UnitOfWork(this._dbContext);

            var sutUser = new UserService(unitOfWorkUser, mapper, loggerUser);
            var sutTask = new TaskService(unitOfWork, logger, mapper);

            // Act
            //var temp = TaskMockData.create_taskEntity();
            UserEntity result = await sutUser.register(UserMockData.NewUserAuthenRequest());
            var result1 = sutTask.create(TaskMockData.create_taskEntity_getAllByUserId_TaskStatus(result));
            var result2 = sutTask.create(TaskMockData.create_taskEntity_getAllByUserId_TaskStatus(result));
            //var result2 = sutTask.create(TaskMockData.create_taskEntity_getAllByUserId(userId, "user02"));
            PaginatedDTO<TaskEntity> result3 = await sutTask.getAllByUserId_TaskStatus(result.Id, 1, 2, TaskStatusConstant.UNCOMPLETED);

            // Assert
            Assert.NotNull(result3);
            Assert.Equal(2, result3.Items.Count());
        }

        [Fact]
        public async Task getAllByUserId()
        {
            // Arrange
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();

            ILogger<TaskService> logger = Mock.Of<ILogger<TaskService>>();
            var unitOfWork = new UnitOfWork(this._dbContext);

            //ILogger<UserService> loggerUser = Mock.Of<ILogger<UserService>>();
            //var unitOfWorkUser = new UnitOfWork(this._dbContext);

            //var sutUser = new UserService(unitOfWorkUser, mapper, loggerUser);
            var sutTask = new TaskService(unitOfWork, logger, mapper);

            // Act
            //var temp = TaskMockData.create_taskEntity();
            Guid userId = Guid.NewGuid();
            var result1 = sutTask.create(TaskMockData.create_taskEntity_getAllByUserId(userId, "user01"));
            //var result2 = sutTask.create(TaskMockData.create_taskEntity_getAllByUserId(userId, "user02"));
            PaginatedDTO<TaskEntity> result = await sutTask.getAllByUserId(userId, 1, 1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Items.Count());
        }

        [Fact]
        public async Task getAll()
        {
            // Arrange
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();
            ILogger<TaskService> logger = Mock.Of<ILogger<TaskService>>();
            var unitOfWork = new UnitOfWork(this._dbContext);

            var sut = new TaskService(unitOfWork, logger, mapper);

            // Act
            //var temp = TaskMockData.create_taskEntity();
            var result1 = sut.create(TaskMockData.create_taskEntity());
            var result2 = sut.create(TaskMockData.create_taskEntity());
            PaginatedDTO<TaskEntity> result = await sut.getAll(1, 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Items.Count());
        }

        [Fact]
        public async Task create()
        {
            // Arrange
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();
            //UserEntity userEntity = mapper.Map<UserEntity>(UserMockData.NewUserAuthenRequest());
            ILogger<TaskService> logger = Mock.Of<ILogger<TaskService>>();
            var unitOfWork = new UnitOfWork(this._dbContext);

            var sut = new TaskService(unitOfWork, logger, mapper);

            // Act
            var result = sut.create(TaskMockData.create_taskEntity());

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task update()
        {
            // Arrange
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();
            ILogger<TaskService> logger = Mock.Of<ILogger<TaskService>>();
            var unitOfWork = new UnitOfWork(this._dbContext);

            var sut = new TaskService(unitOfWork, logger, mapper);

            // Act
            var temp = TaskMockData.create_taskEntity();
            var result = sut.create(temp);
            var result1 = sut.update(TaskMockData.update_taskEntity(temp.Id));

            // Assert
            Assert.NotNull(result1);
        }

        [Fact]
        public async Task delete()
        {
            // Arrange
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();
            ILogger<TaskService> logger = Mock.Of<ILogger<TaskService>>();
            var unitOfWork = new UnitOfWork(this._dbContext);

            var sut = new TaskService(unitOfWork, logger, mapper);

            // Act
            var temp = TaskMockData.create_taskEntity();
            var result = sut.create(temp);
            var result1 = sut.delete(temp.Id);

            // Assert
            Assert.NotNull(result1);
        }

        public void Dispose()
        {
            this._dbContext.Database.EnsureDeleted();
            this._dbContext.Dispose();
        }
    }
}
