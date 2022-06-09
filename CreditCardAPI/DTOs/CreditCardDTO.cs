namespace CreditCardAPI.DTOs
{
    public class CreditCardDTO : BaseDTO
    {
        public CustomerDTO Customer { get; set; }
        public string Number { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

    }
}
