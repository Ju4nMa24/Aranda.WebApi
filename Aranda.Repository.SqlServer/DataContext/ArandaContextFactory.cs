using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Aranda.Repository.SqlServer.DataContext
{
    /// <summary>
    /// Context Factory Db.
    /// </summary>
    public class ArandaContextFactory : IDesignTimeDbContextFactory<ArandaContext>
    {
        /// <summary>
        /// Create the db contexty.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public ArandaContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "../../Aranda.WebApi/Aranda.WebApi/appsettings.json")).Build();
            DbContextOptionsBuilder<ArandaContext> optionBuilder = new();
            optionBuilder.UseSqlServer(configuration["connectionString"]);
            return new ArandaContext(optionBuilder.Options);
        }
    }
}
