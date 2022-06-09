using CreditCardAPI.Models;
using CreditCardAPI.Repositories.Interfaces;

namespace CreditCardAPI.Repositories
{
    public class CustomerRepository : IBaseRepository<Customer>
    {
        public List<Customer> list => throw new NotImplementedException();

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> Upsert(Customer item)
        {
            throw new NotImplementedException();
        }
    }
}
