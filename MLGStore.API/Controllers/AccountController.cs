using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLGStore.API.Common;
using MLGStore.Services.DTOs;
using MLGStore.Services.Interfaces;

namespace MLGStore.API.Controllers
{
    public class AccountController : BaseAPIController
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var result = await accountService.LoginAsync(loginRequest);

            if (result.Success)
            {
                if (!String.IsNullOrEmpty(result.Data))
                    return Ok(result);
                
                return Unauthorized(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }
}
