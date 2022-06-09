using CreditCardAPI.Data;
using CreditCardAPI.Models;
using CreditCardAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CreditCardAPI.Repositories
{
    public class CreditCardRepository : IBaseRepository<CreditCard>
    {
        private readonly ApiContext _context;

        public CreditCardRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CreditCard> GetById(int id)
        {
            return await _context.CreditCards.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CreditCard> GetByNumber(string number)
        {
            return await _context.CreditCards.FirstOrDefaultAsync(x => x.Number == number);
        }

        public async Task<CreditCard> Upsert(CreditCard item)
        {
            try
            {
                var creditCard = _context.CreditCards.Where(x => x.Number == item.Number).FirstOrDefault();

                if (creditCard == null)
                {
                    _context.CreditCards.Add(item);
                }
                else
                {
                    _context.Update(item);
                }

                _context.SaveChanges();

                return item;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error! {ex.Message}");
            }
        }
    }
}
