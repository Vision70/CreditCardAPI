using CreditCardAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CreditCardAPI.Helpers
{
    public class Utils
    {
        public static void RightRotate(int[] arr)
        {
            int tmp = arr[arr.Length - 1];
            for (int i = arr.Length - 1; i > 0; i--)
            {
                arr[i] = arr[i - 1];
            }
            arr[0] = tmp;
        }

        public static string GenerateJwtToken(string key)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes("SecretKey_CreditCardAPI");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("key", key) }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static bool ValidateJwtToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var keySecret = Encoding.ASCII.GetBytes("SecretKey_CreditCardAPI");
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keySecret),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var key = jwtToken.Claims.First(x => x.Type == "key").Value;

                if (key == "")
                    return false;

                return true;
            }
            catch
            {
                throw new Exception("Invalid Token!");
            }
        }

        public static string GetTokenKey(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var keySecret = Encoding.ASCII.GetBytes("SecretKey_CreditCardAPI");
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keySecret),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                return jwtToken.Claims.First(x => x.Type == "key").Value;

            }
            catch
            {
                throw new Exception("Invalid Token!");
            }
        }

        public static void CreateData(DbContext context)
        {
            var customer = new Customer("Marcelo", "Ando");

            context.Add(customer);
            context.SaveChanges();

        }
    }
}
