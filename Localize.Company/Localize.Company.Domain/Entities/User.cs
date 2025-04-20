namespace Localize.Company.Domain.Entities
{
    public class User : EntityBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        //Config One to Many
        public IEnumerable<Organization>? Organizations { get; set; }
    }
}
