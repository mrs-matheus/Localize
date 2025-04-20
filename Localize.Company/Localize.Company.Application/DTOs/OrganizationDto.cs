using Localize.Company.Domain.Entities;

namespace Localize.Company.Application.DTOs
{
    public class OrganizationDto
    {
        public UserDto User { get; set; }
        public IEnumerable<Organization> Organizations { get; set; }

    }
}
