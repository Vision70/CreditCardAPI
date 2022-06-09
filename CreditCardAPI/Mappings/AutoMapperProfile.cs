using AutoMapper;
using CreditCardAPI.DTOs;
using CreditCardAPI.Models;

namespace CreditCardAPI.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreditCard, CreditCardDTO>().ReverseMap();
            CreateMap<Customer, CustomerDTO>().ReverseMap();
        }
    }
}
