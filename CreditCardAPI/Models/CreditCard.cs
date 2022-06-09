using CreditCardAPI.Helpers;
using System.ComponentModel.DataAnnotations;

namespace CreditCardAPI.Models
{
    public class CreditCard : BaseModel
    {
        public CreditCard(Customer customer, string number, int month, int year)
        {
            Customer = customer;
            Number = number;
            Month = month;
            Year = year;
        }

        public CreditCard(Customer customer, string number)
        {
            Customer = customer;
            Number = number;
            Month = 0;
            Year = 0;
        }

        protected CreditCard()
        {
        }

        public Customer Customer { get; private set; }
        [MaxLength(16, ErrorMessage = "Credit card number must have up to 16 digits")]
        public string Number { get; private set; }
        public int Month { get; private set; }
        public int Year { get; private set; }

    }
}
