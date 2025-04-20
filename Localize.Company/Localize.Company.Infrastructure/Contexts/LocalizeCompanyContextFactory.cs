using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Localize.Company.Infrastructure.Contexts
{
    public class LocalizeCompanyContextFactory : IDesignTimeDbContextFactory<LocalizeCompanyContext>
    {
        public LocalizeCompanyContext CreateDbContext(string[] args)
        {

            string basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Localize.Company.Api");

            string jsonFilePath = Path.Combine(basePath, "appsettings.json");

            var config = new ConfigurationManager();
            config.AddJsonFile(jsonFilePath, optional: false, reloadOnChange: true);

            var optionsBuilder = new DbContextOptionsBuilder<LocalizeCompanyContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            return new LocalizeCompanyContext(optionsBuilder.Options);
        }
    }
}
