using Localize.Company.Application.Utils;
using Localize.Company.Domain.Entities;

namespace Localize.Company.Application.DTOs
{
    public class OrganizationDto
    {
        public UserDto User { get; set; }
        public PagedResult<Organization> Organizations { get; set; }

    }
}
