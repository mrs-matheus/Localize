namespace Localize.Company.Domain.Entities
{
    public class UserOrganization : EntityBase
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}