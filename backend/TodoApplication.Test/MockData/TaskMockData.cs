using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApplication.Constants;
using TodoApplication.Entities;
using TodoApplication.Enums;
using TodoApplication.Models.DTOs.Requests;

namespace TodoApplication.Test.MockData
{
    public class TaskMockData
    {
        public static TaskEntity create_taskEntity_getAllByUserId_TaskStatus(UserEntity userEntity)
        {
            return new TaskEntity
            {
                Id = Guid.NewGuid(),
                Name = "task01",
                Description = "null",
                CreatedDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(1),
                Status = TaskStatusConstant.UNCOMPLETED,
                userEntity = userEntity,
            };
        }

        public static TaskEntity create_taskEntity_getAllByUserId(Guid userId, string username)
        {
            return new TaskEntity
            {
                Id = Guid.NewGuid(),
                Name = "task01",
                Description = "null",
                CreatedDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(1),
                Status = TaskStatusConstant.UNCOMPLETED,
                userEntity = new UserEntity
                {
                    Id = userId,
                    Username = username,
                    Password = "user",
                }
            };
        }

        public static TaskEntity update_taskEntity(Guid id)
        {
            return new TaskEntity
            {
                Id = id,
                Name = "task01_update",
                Description = "null",
                CreatedDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(1),
                Status = TaskStatusConstant.UNCOMPLETED,
                userEntity = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Username = "User",
                    Password = "user",
                }
            };
        }

        public static TaskEntity create_taskEntity()
        {
            return new TaskEntity {
                Id = Guid.NewGuid(),
                Name = "task01",
                Description = "null",
                CreatedDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(1),
                Status = TaskStatusConstant.UNCOMPLETED,
                userEntity = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Username = "User",
                    Password = "user",
                }
            };
        }

        public static QueryParamsRequest queryParamsRequest()
        {
            return new QueryParamsRequest
            {
                Page = 2,
                ItemsPerPage = 1
            };
        }

        public static UpdateTaskRequest updateTaskRequest()
        {
            return new UpdateTaskRequest
            {
                Id = Guid.NewGuid(),
                Name = "Task01",
                Status = TaskStatusConstant.UNCOMPLETED,
            };
        }

        public static UpdateTaskRequest updateTaskRequest_NullParams()
        {
            return new UpdateTaskRequest
            {
                Name = "Task01",
                Status = TaskStatusConstant.UNCOMPLETED,
            };
        }

        public static CreateTaskRequest createTaskRequest()
        {
            return new CreateTaskRequest {
                Name = "Task01",
                CreatedDate = DateTime.Now,
                Status = TaskStatusConstant.UNCOMPLETED,
                UserId = Guid.NewGuid(),
            };
        }

        public static CreateTaskRequest createTaskRequest_NullParams()
        {
            return new CreateTaskRequest
            {
                Status = TaskStatusConstant.UNCOMPLETED,
                UserId = Guid.NewGuid(),
            };
        }
    }
}
