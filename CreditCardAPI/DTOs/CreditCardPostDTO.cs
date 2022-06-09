using System.ComponentModel.DataAnnotations;

namespace CreditCardAPI.DTOs
{
    public class CreditCardPostDTO
    {
        public int CustomerId { get; set; }
        [MaxLength(16, ErrorMessage = "Max. 16 digits")]
        public string CardNumber { get; set; }
        [MaxLength(5, ErrorMessage = "Max. 5 digits")]
        public string Cvv { get; set; }
    }
}
