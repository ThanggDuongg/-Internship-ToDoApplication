namespace TodoApplication.Services.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        TodoDBContext _context { get; }
        Task Commit();
    }
}
