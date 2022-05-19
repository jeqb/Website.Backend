namespace Website.Backend.Domain.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();
        // TODO: change this to a string. do the Guid up higher
        // so you can switch over to autoincrementing rowKeys later.
        public Task<T?> GetByIdAsync(Guid id);

        public Task<T> CreateAsync(T entity);

        public Task UpdateAsync(T entity);

        public Task DeleteAsync(T entity);
    }
}
