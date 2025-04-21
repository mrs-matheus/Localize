using Localize.Company.Application.DTOs;
using Localize.Company.Application.Responses;

namespace Localize.Company.Application.Contracts
{
    public interface IAccountService
    {
        Task<ResponseBase<TokenDto>> Create(SignUpDto account);
        Task<ResponseBase<TokenDto>> Authenticate(SignInDto account);
    }
}
