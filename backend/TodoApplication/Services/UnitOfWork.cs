using TodoApplication.Services.Interfaces;

namespace TodoApplication.Services
{
    public class UnitOfWork : IUnitOfWork
    {
#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
        public TodoDBContext? _context { get; }
#pragma warning restore CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).

        public UnitOfWork(TodoDBContext dBContext)
        {
            this._context = dBContext;
        }

        public async Task Commit()
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            await this._context?.SaveChangesAsync();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        public void Dispose()
        {
            this._context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
