using AutoMapper;
using Microsoft.Win32;
using TodoApplication.Entities;
using TodoApplication.Models.DTOs;
using TodoApplication.Models.DTOs.Requests;
using TodoApplication.Services.Interfaces;

namespace TodoApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserService> logger)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._logger = logger;
        }

        public Task create(UserEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedDTO<UserEntity>> getAll(int page = 1, int itemsPerPage = 2)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> getByIdAsync(Guid id)
        {
            if(String.IsNullOrEmpty(id.ToString())) {
                this._logger.LogError("id is invalid.");
                throw new ArgumentNullException("id");
            }

            try
            {
                var userExisted = this._unitOfWork._context.Set<UserEntity>().FirstOrDefault(x => x.Id.CompareTo(id) == 0);
                if (userExisted != null)
                {
                    return Task.FromResult(userExisted);
                }
                else
                {
                    this._logger.LogError("User doesn't existed.");
                    throw new Exception("User doesn't existed.");
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Something Went Wrong in the {nameof(getByIdAsync)}");
                throw new Exception("Something wrong: " + ex.Message);
            }
        }

        public Task<UserEntity> login(UserAuthRequest userAuthenDTO)
        {
            this._logger.LogInformation($"Login: {userAuthenDTO.Username} ");
            if (userAuthenDTO == null)
            {
                this._logger.LogError("DTO is invalid.");
                throw new Exception("DTO is invalid.");
            }

            if (String.IsNullOrEmpty(userAuthenDTO.Username) || String.IsNullOrEmpty(userAuthenDTO.Password))
            {
                this._logger.LogError("Username or Password is invalid.");
                throw new Exception("Username or Password is invalid.");
            }

            try
            {
                var userExisted = this._unitOfWork._context.Set<UserEntity>().FirstOrDefault(
                    u => u.Username == userAuthenDTO.Username
                    &&
                    u.Password == userAuthenDTO.Password
                );

                this._logger.LogInformation("Login successfully");
                if (userExisted != null)
                {
                    return Task.FromResult(userExisted);
                }
                else
                {
                    this._logger.LogError("Username or Password went wrong.");
                    throw new Exception("Username or Password went wrong.");
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Something Went Wrong in the {nameof(login)}");
                throw new Exception("Something wrong: " + ex.Message);
            }
        }

        public async Task<UserEntity> register(UserAuthRequest userAuthenDTO)
        {
            this._logger.LogInformation($"Registration Attempt for {userAuthenDTO.Username} ");
            if (userAuthenDTO == null)
            {
                this._logger.LogError("DTO is invalid.");
                throw new Exception("DTO is invalid.");
            }

            if (String.IsNullOrEmpty(userAuthenDTO.Username) || String.IsNullOrEmpty(userAuthenDTO.Password))
            {
                this._logger.LogError("Username or Password is invalid.");
                throw new Exception("Username or Password is invalid.");
            }

            try {
                var userExisted = this._unitOfWork._context.Set<UserEntity>().SingleOrDefault(u => u.Username == userAuthenDTO.Username);
                if (userExisted is not null)
                {
                    this._logger.LogError("Username has existed.");
                    throw new Exception("Username has existed.");
                }

                var userEntity = this._mapper.Map<UserEntity>(userAuthenDTO);
                await this._unitOfWork._context.Set<UserEntity>().AddAsync(userEntity);
                await this._unitOfWork.Commit();
                this._logger.LogInformation("Register successfully");
                return userEntity;
            }
            catch (Exception ex) {
                this._unitOfWork.Dispose();
                this._logger.LogError($"Something Went Wrong in the {nameof(register)}");
                throw new Exception("Something wrong: " + ex.Message);
            }
        }

        public Task update(UserEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
