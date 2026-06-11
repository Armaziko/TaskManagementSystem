using TaskManagementSystem.Backend.Domain.Generics;

namespace TaskManagementSystem.Backend.Application.Abstractions
{
    /// <summary>
    /// Represents a single, atomic unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// This method returns a repository of type T if it is a valid DbSet.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IRepository<T> Repository<T>() where T : class;
        public int Commit();
        public Task<int> CommitAsync();
    }
}
