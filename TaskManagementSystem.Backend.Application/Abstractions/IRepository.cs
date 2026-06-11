using System.Linq.Expressions;
using TaskManagementSystem.Backend.Domain.Generics;

namespace TaskManagementSystem.Backend.Application.Abstractions
{
    /// <summary>
    /// Represents a generic repository of entity type T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Attempts to add an item to the context asynchronously.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task AddAsync(T item);
        /// <summary>
        /// Attempts to add a range of items to the context asynchronously.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public Task AddRangeAsync(IEnumerable<T> items);
        /// <summary>
        /// Attempts to get an item by id from a context.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An item or a null in case it doesn't exist.</returns>
        public Task<T?> GetByIdAsync(Guid id);
        /// <summary>
        /// Attempts to get a read only list of all items from a context.
        /// </summary>
        /// <returns>A read only list of all items or an empty list if none exist.</returns>
        public Task<IReadOnlyList<T>> GetAllAsync();
        /// <summary>
        /// Returns total count of elements that satisfies given logic. Returns total count if logic is left null.
        /// </summary>
        /// <param name="logic"></param>
        /// <returns></returns>
        public Task<int> GetTotalCount(Expression<Func<T, bool>>? logic = null);

        /// <summary>
        /// Attempts to get a page. This is 1-based pagination. It also takes logic expression for filtering.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limitPerPage"></param>
        /// <returns></returns>
        public Task<IReadOnlyList<T>> GetPage(int page, int limitPerPage, Expression<Func<T, bool>>? logic = null);
        /// <summary>
        /// Attempts to update the item by changing its state in-memory.
        /// </summary>
        /// <param name="item"></param>
        public void Update(T item);
        /// <summary>
        /// Attempts to remove the item by changing its state in-memory.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(T item);
    }
}
