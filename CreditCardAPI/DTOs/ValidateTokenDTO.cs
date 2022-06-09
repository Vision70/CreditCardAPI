namespace CreditCardAPI.DTOs
{
    public class ValidateTokenDTO
    {
        public int CreditCardId { get; set; }
        public int CustomerId { get; set; }
        public string Token { get; set; }
        public string Cvv { get; set; }
    }
}
