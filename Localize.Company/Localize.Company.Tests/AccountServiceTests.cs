using Localize.Company.Application.Contracts;
using Localize.Company.Application.DTOs;
using Localize.Company.Application.Services;
using Localize.Company.Domain.Contracts.Services;
using Localize.Company.Domain.Cryptographs;
using Localize.Company.Domain.Entities;
using Localize.Company.Domain.Notifications;
using Moq;
using System.Linq.Expressions;

namespace Localize.Company.Tests
{
    public class AccountServiceTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly NotificationContext _notification;
        private readonly AccountService _accountService;

        public AccountServiceTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _tokenServiceMock = new Mock<ITokenService>();
            _notification = new NotificationContext();
            _accountService = new AccountService(
                _userServiceMock.Object,
                _tokenServiceMock.Object,
                _notification
            );
        }

        [Fact]
        public async Task Authenticate_ShouldReturnToken_WhenCredentialsAreValid()
        {
            var user = new User
            {
                Id = 1,
                Email = "user@test.com",
                Password = CryptPassword.Hash("123456")
            };

            _userServiceMock
                .Setup(x => x.Get(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new List<User> { user });

            _tokenServiceMock
                .Setup(x => x.GenerateJwtToken(It.IsAny<User>()))
                .Returns("valid_token");

            var signIn = new SignInDto
            {
                Email = "user@test.com",
                Password = "123456"
            };

            var result = await _accountService.Authenticate(signIn);

            Assert.True(result.Success);
            Assert.Equal("valid_token", result.Data.Token);
        }

        [Fact]
        public async Task Authenticate_ShouldFail_WhenUserNotFound()
        {
            _userServiceMock
            .Setup(x => x.Get(It.IsAny<Expression<Func<User, bool>>>()))
            .Returns(Enumerable.Empty<User>());

            var signIn = new SignInDto
            {
                Email = "notfound@test.com",
                Password = "123456"
            };

            var result = await _accountService.Authenticate(signIn);

            Assert.False(result.Success);
            Assert.Contains(result.Errors, e => e.Message.Contains("Email Or Password Incorrect"));
        }

        [Fact]
        public async Task Authenticate_ShouldFail_WhenPasswordIsIncorrect()
        {
            var user = new User
            {
                Id = 1,
                Email = "user@test.com",
                Password = CryptPassword.Hash("senhaCorreta")
            };

            _userServiceMock
                .Setup(x => x.Get(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new List<User> { user });

            var signIn = new SignInDto
            {
                Email = "user@test.com",
                Password = "senhaErrada"
            };

            var result = await _accountService.Authenticate(signIn);

            Assert.False(result.Success = false);
            Assert.Contains(result.Errors, e => e.Message.Contains("Email Or Password Incorrect"));
        }
    }
}
