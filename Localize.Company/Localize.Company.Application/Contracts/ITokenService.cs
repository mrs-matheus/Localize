using Localize.Company.Domain.Entities;

namespace Localize.Company.Application.Contracts
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
    }
}
