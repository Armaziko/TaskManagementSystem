using System.Collections;
using TaskManagementSystem.Backend.Application.Abstractions;
using TaskManagementSystem.Backend.Domain.Generics;

namespace TaskManagementSystem.Backend.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private Hashtable repositories { get; set; } = new();
        private bool _disposed = false;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public int Commit()
        {
            return this._dbContext.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return this._dbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this._dbContext.Dispose();
                }
                this._disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        public IRepository<T> Repository<T>() where T : AggregateRoot
        {
            var type = typeof(T);
            if (!this.repositories.ContainsValue(type))
            {
                var repoType = typeof(Repository<T>);
                var instance = Activator.CreateInstance(repoType.MakeGenericType(type), _dbContext);
                this.repositories.Add(type, instance);
            }
            return (IRepository<T>)repositories[type]!;
        }
    }
}
