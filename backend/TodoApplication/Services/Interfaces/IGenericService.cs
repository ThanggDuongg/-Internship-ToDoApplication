using TodoApplication.Models.DTOs;

namespace TodoApplication.Services.Interfaces
{
    //crud
    public interface IGenericService<T>
    {
        public Task create(T entity);
        
        public Task update(T entity);

        public Task<PaginatedDTO<T>> getAll(int page = 1, int itemsPerPage = 2);

        public Task delete(Guid id);
    }
}
