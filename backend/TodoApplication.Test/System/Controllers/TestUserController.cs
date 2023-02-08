using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApplication.Controllers;
using TodoApplication.Models.DTOs;
using TodoApplication.Services.Interfaces;
using TodoApplication.Test.MockData;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace TodoApplication.Test.System.Controllers
{
    public class TestUserController
    {
        [Fact]
        public async Task Register_ShouldReturn200Status()
        {
            // Arrange
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();

            var userService = new Mock<IUserService>();
            var newUser = UserMockData.NewUserAuthenRequest();
            var config = new Mock<IConfiguration>();
            var sut = new UserController(userService.Object, mapper, config.Object);

            // Act
            var result = (OkObjectResult)await sut.Register(newUser); //UserAuthenDTO

            // Assert
            result.StatusCode.Should().Be(200);
            userService.Verify(x => x.register(newUser), Times.Exactly(1));
        }

        [Fact]
        public async Task Register_ShouldReturn400Status()
        {
            // Arrange
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();

            var userService = new Mock<IUserService>();
            var newUser = UserMockData.NewUserAuthenDTO_EmptyParams();
            var config = new Mock<IConfiguration>();
            var sut = new UserController(userService.Object, mapper, config.Object);

            // Act
            var result = (BadRequestObjectResult)await sut.Register(newUser); //UserAuthenDTO

            // Assert
            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Login_ShouldReturn200Status()
        {
            // Arrange
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();

            var userService = new Mock<IUserService>();
            var newUser = UserMockData.NewUserAuthenRequest();
            var config = new Mock<IConfiguration>();
            var sut = new UserController(userService.Object, mapper, config.Object);

            // Act
            var result = (OkObjectResult)await sut.Login(newUser); //UserAuthenDTO

            // Assert
            result.StatusCode.Should().Be(200);
            userService.Verify(x => x.login(newUser), Times.Exactly(1));
        }

        [Fact]
        public async Task Login_ShouldReturn400Status()
        {
            // Arrange
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();

            var userService = new Mock<IUserService>();
            var newUser = UserMockData.NewUserAuthenDTO_EmptyParams();
            var config = new Mock<IConfiguration>();
            var sut = new UserController(userService.Object, mapper, config.Object);

            // Act
            var result = (BadRequestObjectResult)await sut.Login(newUser); //UserAuthenDTO

            // Assert
            result.StatusCode.Should().Be(400);
            //userService.Verify(x => x.login(newUser), Times.Exactly(1));
        }
    }
}
