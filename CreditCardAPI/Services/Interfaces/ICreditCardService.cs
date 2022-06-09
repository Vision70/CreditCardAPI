using CreditCardAPI.DTOs;
using CreditCardAPI.Repositories;

namespace CreditCardAPI.Services.Interfaces
{
    public interface ICreditCardService
    {
        Task<CreditCardResultDTO> Upsert(CreditCardPostDTO dto);
        Task<CreditCardDTO> GetByNumber(string number);
        Task<bool> ValidateToken(ValidateTokenDTO validateTokenDTO);
    }
}
