using Localize.Company.Application.Contracts;
using Localize.Company.Application.DTOs;
using Localize.Company.Domain.Contracts.Services;
using Localize.Company.Domain.Cryptographs;
using Localize.Company.Domain.Entities;

namespace Localize.Company.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        public AccountService(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        public async Task<string> Create(SignUpDto signUp)
        {
            var user = new User
            {
                Name = signUp.Name,
                Email = signUp.Email,
                Password = CryptPassword.Hash(signUp.Password)
            };

            if (_userService.Get(x => x.Email == signUp.Email).Count() > 0)
            {
                throw new Exception("Try other email");
            }

            var userCreated = await _userService.Add(user);

            var token = _tokenService.GenerateJwtToken(userCreated);

            return token;
        }


        public async Task<string> Authenticate(SignInDto signIn)
        {
            var user = _userService.Get(x => x.Email == signIn.Email).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (!CryptPassword.Verify(signIn.Password, user.Password))
            {
                throw new Exception("Invalid password");
            }
            var token = _tokenService.GenerateJwtToken(user);
            return token;
        }
    }
}
