using Localize.Company.Application.Contracts;
using Localize.Company.Application.DTOs;
using Localize.Company.Application.Responses;
using Localize.Company.Domain.Contracts.Services;
using Localize.Company.Domain.Cryptographs;
using Localize.Company.Domain.Entities;
using Localize.Company.Domain.Notifications;

namespace Localize.Company.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly NotificationContext _notification;
        public AccountService(
            IUserService userService,
            ITokenService tokenService,
            NotificationContext notification)
        {
            _userService = userService;
            _tokenService = tokenService;
            _notification = notification;
        }

        public async Task<ResponseBase<TokenDto>> Create(SignUpDto signUp)
        {
            if (_userService.Get(x => x.Email == signUp.Email).Count() > 0)
            {
                _notification.AddNotification("SignUp", "Email has been used, try other email");
                return ResponseBase<TokenDto>.Fail(_notification.Notifications.ToList());
            }

            var userCreated = await _userService.Add(new User
            {
                Name = signUp.Name,
                Email = signUp.Email,
                Password = CryptPassword.Hash(signUp.Password)
            });

            var token = _tokenService.GenerateJwtToken(userCreated);

            var response = new ResponseBase<TokenDto>
            {
                Data = new TokenDto { Token = token },
                Success = true
            };

            return response;
        }


        public async Task<ResponseBase<TokenDto>> Authenticate(SignInDto signIn)
        {
            var user = _userService.Get(x => x.Email == signIn.Email).FirstOrDefault();
            if (user == null)
            {
                _notification.AddNotification("SignIn", "Email Or Password Incorrect");
                return ResponseBase<TokenDto>.Fail(_notification.Notifications.ToList());
            }
            if (!CryptPassword.Verify(signIn.Password, user.Password))
            {
                _notification.AddNotification("SignIn", "Email Or Password Incorrect");
                return ResponseBase<TokenDto>.Fail(_notification.Notifications.ToList());
            }
            var token = _tokenService.GenerateJwtToken(user);

            var response = new TokenDto
            {
                Token = token
            };

            return ResponseBase<TokenDto>.Ok(response);
        }
    }
}
