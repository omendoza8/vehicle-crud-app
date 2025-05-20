namespace CrudVehiclesApp.Api.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T?> GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChanges();
    }
}
