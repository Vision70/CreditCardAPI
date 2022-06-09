namespace CreditCardAPI.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public int Id { get; }
        public DateTimeOffset CreatedAt { get; }
    }
}
