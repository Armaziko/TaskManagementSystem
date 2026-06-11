using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManagementSystem.Backend.Application.Abstractions;

namespace TaskManagementSystem.Backend.Infrastructure.Persistence
{
    public class Repository<T> : IRepository<T> where T : class
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

        public async Task<IReadOnlyList<T>> GetPage(int page, int limitPerPage, Expression<Func<T, bool>>? logic = null)
        {
            if (logic is null)
            {
                return await this.set.Skip((page-1) * limitPerPage).Take(limitPerPage).AsNoTracking().ToListAsync(); // 1-based ari

            }
            return await this.set.Where(logic).Skip((page-1)*limitPerPage).Take(limitPerPage).AsNoTracking().ToListAsync();
        }

        public async Task<int> GetTotalCount(Expression<Func<T, bool>>? logic = null)
        {
            if (logic is null)
            {
                return await this.set.CountAsync();
            }
            return await this.set.Where(logic).CountAsync();
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
