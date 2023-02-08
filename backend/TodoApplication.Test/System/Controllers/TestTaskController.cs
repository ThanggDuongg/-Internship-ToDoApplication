using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApplication.Constants;
using TodoApplication.Controllers;
using TodoApplication.Entities;
using TodoApplication.Enums;
using TodoApplication.Services.Interfaces;
using TodoApplication.Test.MockData;

namespace TodoApplication.Test.System.Controllers
{
    public class TestTaskController
    {
        [Fact]
        public async Task create_ShouldReturn200Status()
        {
            // Arrange
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();

            var taskService = new Mock<ITaskService>();
            var userService = new Mock<IUserService>();
            var genericService = new Mock<IGenericService<TaskEntity>>();
            var newTask = TaskMockData.createTaskRequest();
            var sut = new TaskController(userService.Object, taskService.Object, mapper);

            // Act
            var result = (OkObjectResult)await sut.create(newTask); //UserAuthenDTO
            var temp = mapper.Map<TaskEntity>(newTask);
            // Assert
            result.StatusCode.Should().Be(200);
            //genericService.Verify(x => x.create(temp), Times.Exactly(1));
        }

        [Fact]
        public async Task create_ShouldReturn400Status()
        {
            // Arrange
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();

            var taskService = new Mock<ITaskService>();
            var userService = new Mock<IUserService>();
            var genericService = new Mock<IGenericService<TaskEntity>>();
            var newTask = TaskMockData.createTaskRequest_NullParams();
            var sut = new TaskController(userService.Object, taskService.Object, mapper);

            // Act
            var result = (BadRequestObjectResult)await sut.create(newTask); //UserAuthenDTO
            var temp = mapper.Map<TaskEntity>(newTask);
            // Assert
            result.StatusCode.Should().Be(400);
            //genericService.Verify(x => x.create(temp), Times.Exactly(1));
        }

        [Fact]
        public async Task update_ShouldReturn200Status()
        {
            // Arrange
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();

            var taskService = new Mock<ITaskService>();
            var userService = new Mock<IUserService>();
            var genericService = new Mock<IGenericService<TaskEntity>>();
            var newTask = TaskMockData.updateTaskRequest();
            var sut = new TaskController(userService.Object, taskService.Object, mapper);

            // Act
            var result = (OkObjectResult)await sut.update(newTask); //UserAuthenDTO
            var temp = mapper.Map<TaskEntity>(newTask);
            // Assert
            result.StatusCode.Should().Be(200);
            //genericService.Verify(x => x.create(temp), Times.Exactly(1));
        }

        [Fact]
        public async Task update_ShouldReturn400Status()
        {
            // Arrange
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();

            var taskService = new Mock<ITaskService>();
            var userService = new Mock<IUserService>();
            var genericService = new Mock<IGenericService<TaskEntity>>();
            var newTask = TaskMockData.updateTaskRequest_NullParams();
            var sut = new TaskController(userService.Object, taskService.Object, mapper);

            // Act
            var result = (BadRequestObjectResult)await sut.update(newTask); //UserAuthenDTO
            var temp = mapper.Map<TaskEntity>(newTask);
            // Assert
            result.StatusCode.Should().Be(400);
            //genericService.Verify(x => x.create(temp), Times.Exactly(1));
        }

        [Fact]
        public async Task delete_ShouldReturn204Status()
        {
            // Arrange
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();

            var taskService = new Mock<ITaskService>();
            var userService = new Mock<IUserService>();
            var genericService = new Mock<IGenericService<TaskEntity>>();
            var sut = new TaskController(userService.Object, taskService.Object, mapper);

            // Act
            var result = (NoContentResult)await sut.delete(Guid.NewGuid().ToString()); //UserAuthenDTO
            // Assert
            result.StatusCode.Should().Be(204);
            //genericService.Verify(x => x.create(temp), Times.Exactly(1));
        }

        [Fact]
        public async Task getAll_ShouldReturn200Status()
        {
            // Arrange
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();

            var taskService = new Mock<ITaskService>();
            var userService = new Mock<IUserService>();
            var genericService = new Mock<IGenericService<TaskEntity>>();
            var newTask = TaskMockData.queryParamsRequest();
            var sut = new TaskController(userService.Object, taskService.Object, mapper);

            // Act
            var result = (OkObjectResult)await sut.getAll(newTask); //UserAuthenDTO
            // Assert
            result.StatusCode.Should().Be(200);
            //genericService.Verify(x => x.create(temp), Times.Exactly(1));
        }

        [Fact]
        public async Task getAllByUserId_ShouldReturn200Status()
        {
            // Arrange
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();

            var taskService = new Mock<ITaskService>();
            var userService = new Mock<IUserService>();
            var genericService = new Mock<IGenericService<TaskEntity>>();
            var newTask = TaskMockData.queryParamsRequest();
            var sut = new TaskController(userService.Object, taskService.Object, mapper);

            // Act
            var result = (OkObjectResult)await sut.getAll(Guid.NewGuid().ToString(), newTask); //UserAuthenDTO
            // Assert
            result.StatusCode.Should().Be(200);
            //genericService.Verify(x => x.create(temp), Times.Exactly(1));
        }

        [Fact]
        public async Task getAllByUserIdAndFilter_ShouldReturn200Status()
        {
            // Arrange
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();

            var taskService = new Mock<ITaskService>();
            var userService = new Mock<IUserService>();
            var genericService = new Mock<IGenericService<TaskEntity>>();
            var newTask = TaskMockData.queryParamsRequest();
            var sut = new TaskController(userService.Object, taskService.Object, mapper);

            // Act
            var result = (OkObjectResult)await sut.getAll(Guid.NewGuid().ToString(), newTask,
                TaskStatusConstant.UNCOMPLETED,
                SortByConstant.DEFAULT
            , OrderConstant.DEFAULT); //UserAuthenDTO
            // Assert
            result.StatusCode.Should().Be(200);
            //genericService.Verify(x => x.create(temp), Times.Exactly(1));
        }
    }
}
