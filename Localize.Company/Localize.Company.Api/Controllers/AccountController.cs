using Localize.Company.Api.Requests;
using Localize.Company.Application.Contracts;
using Localize.Company.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Localize.Company.Controllers
{
    public class AccountController : BaseController
    {
        private IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            var dto = new SignUpDto
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password
            };

            var result = await _accountService.Create(dto);

            return Ok(new TokenDto { Token = result });
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            var dto = new SignInDto
            {
                Email = request.Email,
                Password = request.Password
            };
            var result = await _accountService.Authenticate(dto);

            return Ok(new TokenDto { Token = result });
        }
    }
}
