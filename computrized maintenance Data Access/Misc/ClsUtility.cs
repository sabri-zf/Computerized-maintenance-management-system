using computrized_maintenance_Data_Access.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computrized_maintenance_Data_Access.Misc
{
    public class ClsUtility
    {
        public readonly static string? ConnectionString = new ConfigurationBuilder()
                                               .AddJsonFile("AppSetting.json")
                                               .Build()
                                               .GetSection("ConnectionStr").Value;


        /// <summary>
        /// Create extrenal session With Configuration to connection with Data Base
        /// </summary>
        /// <returns>return : AppDbcontext Class </returns>
       public static AppDbContext? ImplementDbContextService()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(config =>
            {
                config.UseSqlServer(ConnectionString);
            });

            return services.BuildServiceProvider().GetRequiredService<AppDbContext>();
        }
    }
}
