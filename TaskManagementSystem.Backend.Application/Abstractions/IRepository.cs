using TaskManagementSystem.Backend.Domain.Generics;

namespace TaskManagementSystem.Backend.Application.Abstractions
{
    /// <summary>
    /// Represents a generic repository of entity type T that is of type <see cref="AggregateRoot"></see>. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : AggregateRoot // რათა მივყვეთ სუფთა DDD - ს.
                                                            // თუკი რამე კონკრეტული არა აგრეგატისთვის იქნება საჭირო რეფო
                                                            // ცალკე უნდა შეიქმნას.
    {
        public Task AddAsync(T item);
        public Task AddRangeAsync(IEnumerable<T> items);
        public Task<T?> GetByIdAsync(Guid id);
        public Task<IReadOnlyList<T>> GetAllAsync();
        public void Update(T item);
        public void Remove(T item);
    }
}
