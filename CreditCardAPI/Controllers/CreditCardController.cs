using CreditCardAPI.DTOs;
using CreditCardAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditCardController : ControllerBase
    {
        private readonly ILogger<CreditCardController> _logger;
        private readonly ICreditCardService _creditCardService;

        public CreditCardController(ILogger<CreditCardController> logger, ICreditCardService creditCardService)
        {
            _logger = logger;
            _creditCardService = creditCardService;
        }

        [HttpPost(Name = "Save")]
        public async Task<IActionResult> Save(CreditCardPostDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("Error! Some credit card information is missing");
                }

                var creditCard = await _creditCardService.Upsert(dto);
                if (creditCard == null)
                    return BadRequest("Error! Could not save the credit card");

                return Ok(creditCard);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error! {ex.Message}");
            }
        }

        [HttpGet("ValidateToken/{token}")]
        public async Task<IActionResult> ValidateToken(ValidateTokenDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Error!");
            }

            var result = await _creditCardService.ValidateToken(dto);

            return Ok(result);
        }
    }
}