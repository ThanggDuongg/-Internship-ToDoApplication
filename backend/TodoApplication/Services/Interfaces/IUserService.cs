using TodoApplication.Entities;
using TodoApplication.Models.DTOs.Requests;

namespace TodoApplication.Services.Interfaces
{
    //crud
    public interface IUserService : IGenericService<UserEntity>
    {
        //public Task<UserEntity> GetByIdAsync(int id);

        //public Task<UserEntity> GetByNameAsync(string name);

        public Task<UserEntity> login(UserAuthRequest userAuthenDTO);

        public Task<UserEntity> register(UserAuthRequest userAuthenDTO);

        public Task<UserEntity> getByIdAsync(Guid id);
    }
}
