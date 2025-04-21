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
            var result = await _accountService.Create(new SignUpDto
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password
            });

            if (result.Success == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            var result = await _accountService.Authenticate(new SignInDto
            {
                Email = request.Email,
                Password = request.Password
            });

            if(result.Success == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
