using TaskManagementSystem.Backend.Domain.Generics;

namespace TaskManagementSystem.Backend.Application.Abstractions
{
    /// <summary>
    /// Represents a single, atomic unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<T> Repository<T>() where T : AggregateRoot;
        public int Commit();
        public Task<int> CommitAsync();
    }
}
