using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Backend.Application.Abstractions;
using TaskManagementSystem.Backend.Domain.Generics;

namespace TaskManagementSystem.Backend.Infrastructure.Persistence
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> set;
        public Repository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.set = _dbContext.Set<T>();
        }
        public async Task AddAsync(T item)
        {
            await this.set.AddAsync(item);
        }

        public async Task AddRangeAsync(IEnumerable<T> items)
        {
            await this.set.AddRangeAsync(items);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await this.set.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await this.set.FindAsync(id);
        }

        public void Remove(T item)
        {
            this.set.Remove(item);
        }

        public void Update(T item)
        {
            this.set.Update(item);
        }
    }
}
