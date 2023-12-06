using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MLGStore.Data;
using MLGStore.Entities;
using MLGStore.Services.Common;
using MLGStore.Services.DTOs;
using MLGStore.Services.Helpers;
using MLGStore.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MLGStore.Services.Services
{
    public class AccountService : ServiceBase, IAccountService
    {
        private readonly IPasswordHasher<Customer> passwordHasher;
        private readonly JWT jwt;

        public AccountService(StoreDbContext dbContext,
            ILogger<AccountService> logger,
            IPasswordHasher<Customer> passwordHasher,
            IOptions<JWT> jwt)
            : base(dbContext, logger)
        {
            this.passwordHasher = passwordHasher;
            this.jwt = jwt.Value;
        }

        public async Task<Result<string>> LoginAsync(LoginRequestDTO loginRequest)
        {
            try
            {
                var customer = await dbContext.Customers
                    .FirstOrDefaultAsync(x => x.Username == loginRequest.Username);

                if (customer == null)
                    return Result<string>
                        .CreateResult(string.Empty, "Invalid username or password");

                if (passwordHasher.VerifyHashedPassword(customer, customer.Password, loginRequest.Password) != PasswordVerificationResult.Success)
                    return Result<string>
                        .CreateResult(string.Empty, "Invalid username or password");

                var token = GenerateToken(customer);

                return Result<string>.CreateResult(new JwtSecurityTokenHandler().WriteToken(token));
            }
            catch (Exception ex)
            {
                return Result<string>.CreateExceptionResult(ex);
            }
        }

        private JwtSecurityToken GenerateToken(Customer customer)
        {
            var claims = new[]
        {
                                new Claim(JwtRegisteredClaimNames.Sub, customer.Username),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim("customerid", customer.Id.ToString()),
                                new Claim("username", customer.Username)
                        };

            var now = DateTime.Now;

            var ssk = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signinCredentials = new SigningCredentials(ssk, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
            expires: now.AddDays(1),
                signingCredentials: signinCredentials);

            return token;
        }

    }
}