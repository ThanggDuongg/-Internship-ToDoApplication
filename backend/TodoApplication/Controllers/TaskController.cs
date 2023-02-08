using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using TodoApplication.Constants;
using TodoApplication.Entities;
using TodoApplication.Enums;
using TodoApplication.Filters;
using TodoApplication.Models;
using TodoApplication.Models.DTOs;
using TodoApplication.Models.DTOs.Requests;
using TodoApplication.Models.DTOs.Responses;
using TodoApplication.Services.Interfaces;

namespace TodoApplication.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TaskController(IUserService userService, ITaskService taskService, IMapper mapper)
        {
            this._userService = userService;
            this._mapper = mapper;
            this._taskService = taskService;
        }

        [HttpGet("getAllByUserIdAndStatusTask")]
        public async Task<IActionResult> getAll(String UserId,
            [FromQuery] QueryParamsRequest queryParamsRequest,
            string taskStatus = TaskStatusConstant.UNCOMPLETED,
            string sortBy = SortByConstant.DEFAULT
            , string orderBy = OrderConstant.DEFAULT
             )
        {
            try
            {
                PaginatedDTO<TaskEntity> temp = await this._taskService.getAllByUserId_TaskStatus(Guid.Parse(UserId), queryParamsRequest.Page, queryParamsRequest.ItemsPerPage,
                    taskStatus
                    , orderBy, sortBy
                    );
                return Ok((new Response<PaginatedDTO<TaskEntity>>
                {
                    Success = true,
                    Data = temp,
                    Message = "get all Tasks by User Id successfully !"
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<Exception>
                {
                    Success = false,
                    Message = ex.Message,
                });
            }
        }

        [HttpGet("getAllByUserId")]
        public async Task<IActionResult> getAll(String UserId, [FromQuery] QueryParamsRequest queryParamsRequest)
        {
            try
            {
                PaginatedDTO<TaskEntity> temp = await this._taskService.getAllByUserId(Guid.Parse(UserId), queryParamsRequest.Page, queryParamsRequest.ItemsPerPage);
                return Ok((new Response<PaginatedDTO<TaskEntity>>
                {
                    Success = true,
                    Data = temp,
                    Message = "get all Tasks by User Id successfully !"
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<Exception>
                {
                    Success = false,
                    Message = ex.Message,
                });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> getAll([FromQuery] QueryParamsRequest queryParamsRequest)
        {
            try
            {
                PaginatedDTO<TaskEntity> temp = await this._taskService.getAll(queryParamsRequest.Page, queryParamsRequest.ItemsPerPage);
                return Ok((new Response<PaginatedDTO<TaskEntity>>
                {
                    Success = true,
                    Data = temp,
                    Message = "get all Tasks successfully !"
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<Exception>
                {
                    Success = false,
                    Message = ex.Message,
                });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> delete(string id)
        {
            try
            {
                await this._taskService.delete(Guid.Parse(id));
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<Exception>
                {
                    Success = false,
                    Message = ex.Message,
                });
            }
        }

        [HttpPut]
        //[ValidationFilterAttribute]
        public async Task<IActionResult> update([FromBody] UpdateTaskRequest updateTaskRequest)
        {
            if (updateTaskRequest?.Id == null || updateTaskRequest?.Id == Guid.Empty)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return BadRequest(new Response<UpdateTaskResponse>
                {
                    Success = false,
                    Message = "Something wrong ...",
                    Errors = allErrors
                });
            }

            try
            {
                TaskEntity task = this._mapper.Map<TaskEntity>(updateTaskRequest);
                await this._taskService.update(task);

                return Ok(new Response<UpdateTaskResponse>
                {
                    Success = true,
                    Data = this._mapper.Map<UpdateTaskResponse>(task),
                    Message = "update Task successfully !"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<Exception>
                {
                    Success = false,
                    Message = ex.Message,
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> create([FromBody] CreateTaskRequest createTaskRequest)
        {
            if (String.IsNullOrEmpty(createTaskRequest.Name) || createTaskRequest?.UserId == null ||
                        createTaskRequest?.Status == null || createTaskRequest.CreatedDate == null)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return BadRequest(new Response<CreateTaskResponse>
                {
                    Success = false,
                    Message = "Something wrong ...",
                    Errors = allErrors
                });
            }

            try
            {
                TaskEntity task = this._mapper.Map<TaskEntity>(createTaskRequest);
                //task.Status = createTaskDTO.Status;
                var userEntity = await this._userService.getByIdAsync(createTaskRequest.UserId);
                task.userEntity = userEntity;

                await this._taskService.create(task);

                return Ok(new Response<CreateTaskResponse>
                {
                    Success = true,
                    Data = this._mapper.Map<CreateTaskResponse>(task),
                    Message = "create new Task successfully !"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<Exception>
                {
                    Success = false,
                    Message = ex.Message,
                });
            }
        }
    }
}
