using AutoMapper;
using CreditCardAPI.DTOs;
using CreditCardAPI.Helpers;
using CreditCardAPI.Models;
using CreditCardAPI.Repositories;
using CreditCardAPI.Services.Interfaces;

namespace CreditCardAPI.Services
{
    public class CredtiCardService : ICreditCardService
    {
        private readonly CreditCardRepository _creditCardRepository;
        private readonly CustomerRepository _customerRepository;
        public readonly IMapper _mapper;

        public CredtiCardService(CreditCardRepository creditCardRepository, CustomerRepository customerRepository, IMapper mapper)
        {
            _creditCardRepository = creditCardRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CreditCardDTO> GetByNumber(string number)
        {
            var creditCard = _creditCardRepository.GetByNumber(number);
            return _mapper.Map<CreditCardDTO>(creditCard);
        }

        public async Task<CreditCardResultDTO> Upsert(CreditCardPostDTO dto)
        {
            try
            {
                var creditCard = await _creditCardRepository.GetByNumber(dto.CardNumber);

                if (creditCard == null)
                {
                    var customer = await _customerRepository.GetById(dto.CustomerId);
                    if (customer == null)
                    {
                        throw new Exception("Customer not found.");
                    }

                    creditCard = new CreditCard(customer, dto.CardNumber);
                    await _creditCardRepository.Upsert(creditCard);
                }

                var arrLast4 = creditCard.Number.Substring(creditCard.Number.Length - 4)
                                             .ToCharArray()
                                             .Select(d => int.Parse(d.ToString()))
                                             .ToArray();
                var cvv = int.Parse(dto.Cvv);
                for (int i = cvv % arrLast4.Length; i < arrLast4.Length; i++)
                    Utils.RightRotate(arrLast4);

                var strLast4 = String.Concat(arrLast4.Select(d => d.ToString()));

                var token = Utils.GenerateJwtToken(strLast4);

                CreditCardResultDTO result = new CreditCardResultDTO()
                {
                    CreditCardId = creditCard.Id,
                    RegistrationDate = DateTime.UtcNow,
                    Token = token
                };

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error! {ex.Message}");
            }
        }

        public async Task<bool> ValidateToken(ValidateTokenDTO validateTokenDTO)
        {
            try
            {
                if (!Utils.ValidateJwtToken(validateTokenDTO.Token))
                    return false;

                var creditCard = await _creditCardRepository.GetById(validateTokenDTO.CreditCardId);
                if (creditCard == null)
                    return false;

                var customer = await _customerRepository.GetById(validateTokenDTO.CustomerId);
                if (customer == null)
                    return false;

                var arrLast4 = creditCard.Number.Substring(creditCard.Number.Length - 4)
                                             .ToCharArray()
                                             .Select(d => int.Parse(d.ToString()))
                                             .ToArray();

                var cvv = int.Parse(validateTokenDTO.Cvv);
                for (int i = cvv % arrLast4.Length; i < arrLast4.Length; i++)
                    Utils.RightRotate(arrLast4);

                var strLast4 = String.Concat(arrLast4.Select(d => d.ToString()));

                var tokenKey = Utils.GetTokenKey(validateTokenDTO.Token);

                if (strLast4 != tokenKey)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
