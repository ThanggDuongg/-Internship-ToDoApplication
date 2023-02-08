using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoApplication.Constants;
using TodoApplication.Entities;
using TodoApplication.Enums;
using TodoApplication.Models.DTOs;
using TodoApplication.Services.Interfaces;

namespace TodoApplication.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TaskService> _logger;
        private readonly IMapper _mapper;

        public TaskService(IUnitOfWork unitOfWork, ILogger<TaskService> logger, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
            this._mapper = mapper;
        }

        public async Task create(TaskEntity entity)
        {
            if(entity == null)
            {
                this._logger.LogError("DTO is invalid.");
                throw new ArgumentNullException("Task is invalid");
            }

            try
            {
                await this._unitOfWork._context.Set<TaskEntity>().AddAsync(entity);
                await this._unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                this._unitOfWork.Dispose();
                this._logger.LogError($"Something Went Wrong in the {nameof(create)}");
                throw new Exception("Something wrong: " + ex.Message);
            }
        }

        public async Task delete(Guid id)
        {
            if (String.IsNullOrEmpty(id.ToString()))
            {
                this._logger.LogError("id is invalid.");
                throw new ArgumentNullException("id is invalid");
            }

            try
            {
                var taskEntity = this._unitOfWork._context.Set<TaskEntity>().FirstOrDefault(t => t.Id.Equals(id));
                
                if (taskEntity != null)
                {
                    this._unitOfWork._context.Set<TaskEntity>().Remove(taskEntity);
                    await this._unitOfWork.Commit();
                }
                else
                {
                    this._logger.LogError($"Something Went Wrong in the {nameof(delete)}");
                    throw new Exception("Something wrong");
                }
            }
            catch (Exception ex)
            {
                this._unitOfWork.Dispose();
                this._logger.LogError($"Something Went Wrong in the {nameof(delete)}");
                throw new Exception("Something wrong: " + ex.Message);
            }
        }

        public async Task<PaginatedDTO<TaskEntity>> getAll(int page = 1, int itemsPerPage = 2)
        {
            try
            {
                // Eager load
                IEnumerable<TaskEntity> taskEntities = await this._unitOfWork._context.Set<TaskEntity>().Include(x => x.userEntity).ToListAsync().ConfigureAwait(false);
                return PaginatedDTO<TaskEntity>.ToPaginatedPost(
                    taskEntities.AsQueryable(), page, itemsPerPage);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Something Went Wrong in the {nameof(getAll)}");
                throw new Exception("Something wrong: " + ex.Message);
            }
        }

        public async Task<PaginatedDTO<TaskEntity>> getAllByUserId(Guid userId, int page = 1, int itemsPerPage = 2)
        {
            if (String.IsNullOrEmpty(userId.ToString()))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            try
            {
                var userExisted = await this._unitOfWork._context.Set<UserEntity>().FirstOrDefaultAsync(x => x.Id.Equals(userId));

                // Eager load
                if (userExisted != null)
                {
                    IEnumerable<TaskEntity> taskEntities = await this._unitOfWork._context.Set<TaskEntity>().Include(x => x.userEntity).Where(x => x.userEntity.Id.Equals(userId)).ToListAsync().ConfigureAwait(false);
                    return PaginatedDTO<TaskEntity>.ToPaginatedPost(
                        taskEntities.AsQueryable(), page, itemsPerPage);
                }
                else
                {
                    throw new Exception("User not existed.");
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Something Went Wrong in the {nameof(getAll)}");
                throw new Exception("Something wrong: " + ex.Message);
            }
        }

        public async Task<PaginatedDTO<TaskEntity>> getAllByUserId_TaskStatus(Guid userId, int page = 1, int itemsPerPage = 2,
            string taskStatus = TaskStatusConstant.UNCOMPLETED, string orderBy = OrderConstant.DEFAULT, string sortBy = SortByConstant.DEFAULT
            )
        {
            if (String.IsNullOrEmpty(userId.ToString()))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            orderBy = orderBy.ToUpper();
            sortBy = sortBy.ToUpper();

            try
            {
                var userExisted = await this._unitOfWork._context.Set<UserEntity>().FirstOrDefaultAsync(x => x.Id.Equals(userId));

                // Eager load
                if (userExisted != null)
                {
                    var taskEntities = 
                        this._unitOfWork._context.Set<TaskEntity>().Include(x => x.userEntity).
                        Where(x => x.userEntity.Id.Equals(userId) && x.Status == taskStatus);
                    //ToListAsync().ConfigureAwait(false);

                    switch (sortBy)
                    {
                        case SortByConstant.DUE_DATE:
                            if (OrderConstant.ASC.Equals(orderBy))
                            {
                                taskEntities = taskEntities.OrderBy(x => x.DueDate);
                            }
                            else
                            {
                                taskEntities = taskEntities.OrderByDescending(x => x.DueDate);
                            }
                            break;
                        case SortByConstant.DATE_ADDED:
                            if (OrderConstant.ASC.Equals(orderBy))
                            {
                                taskEntities = taskEntities.OrderBy(x => x.CreatedDate);
                            }
                            else
                            {
                                taskEntities = taskEntities.OrderByDescending(x => x.CreatedDate);
                            }
                            break;
                        default:
                            if(OrderConstant.ASC.Equals(orderBy))
                            {
                                taskEntities = taskEntities.OrderBy(x => x.Name);
                            }
                            else
                            {
                                taskEntities = taskEntities.OrderByDescending(x => x.Name);
                            }
                            break;
                    }
                    //taskEntities = await taskEntities.ToListAsync().ConfigureAwait(false);
                    return PaginatedDTO<TaskEntity>.ToPaginatedPost(
                        taskEntities.AsQueryable(), page, itemsPerPage);
                }
                else
                {
                    throw new Exception("User not existed.");
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Something Went Wrong in the {nameof(getAll)}");
                throw new Exception("Something wrong: " + ex.Message);
            }
        }

        public async Task update(TaskEntity entity)
        {
            if (entity == null)
            {
                this._logger.LogError("DTO is invalid.");
                throw new ArgumentNullException("Task is invalid");
            }

            try
            {
                var result = this._unitOfWork._context.Set<TaskEntity>().SingleOrDefault(t => t.Id.CompareTo(entity.Id) == 0);
                
                if (result != null)
                {
                    result.Name = entity.Name;
                    result.Description = entity.Description;
                    result.Status = entity.Status;
                    result.DueDate = entity.DueDate;

                    this._unitOfWork._context.Set<TaskEntity>().Update(result);
                    await this._unitOfWork.Commit();
                }
                else
                {
                    this._logger.LogError($"Something Went Wrong in the {nameof(update)}");
                    throw new Exception("Task does not existed");
                }
            }
            catch (Exception ex)
            {
                this._unitOfWork.Dispose();
                this._logger.LogError($"Something Went Wrong in the {nameof(create)}");
                throw new Exception("Something wrong: " + ex.Message);
            }
        }
    }
}
