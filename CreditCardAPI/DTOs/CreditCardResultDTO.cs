namespace CreditCardAPI.DTOs
{
    public class CreditCardResultDTO
    {
        public int CreditCardId { get; set; }
        public DateTimeOffset RegistrationDate { get; set; } = DateTimeOffset.UtcNow;
        public string Token { get; set; } = string.Empty;
    }
}
