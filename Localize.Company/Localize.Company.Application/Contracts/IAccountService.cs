using Localize.Company.Application.DTOs;

namespace Localize.Company.Application.Contracts
{
    public interface IAccountService
    {
        Task<string> Create(SignUpDto account);
        Task<string> Authenticate(SignInDto account);
    }
}
