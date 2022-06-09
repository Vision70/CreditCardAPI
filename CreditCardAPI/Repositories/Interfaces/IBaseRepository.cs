namespace CreditCardAPI.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<T> GetById(int id);
        public Task<T> Upsert(T item);
        public Task<bool> Delete(int id);
    }
}
