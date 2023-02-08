using TodoApplication.Constants;
using TodoApplication.Entities;
using TodoApplication.Enums;
using TodoApplication.Models.DTOs;

namespace TodoApplication.Services.Interfaces
{
    public interface ITaskService : IGenericService<TaskEntity>
    {
        public Task<PaginatedDTO<TaskEntity>> getAllByUserId(Guid userId, int page = 1, int itemsPerPage = 2);

        public Task<PaginatedDTO<TaskEntity>> getAllByUserId_TaskStatus(
            Guid userId,
            int page = 1,
            int itemsPerPage = 2,
            string taskStatus = TaskStatusConstant.UNCOMPLETED,
            string orderBy = OrderConstant.DEFAULT,
            string sortBy = SortByConstant.DEFAULT
            );
    }
}
