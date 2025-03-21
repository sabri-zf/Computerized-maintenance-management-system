using Microsoft.Extensions.Configuration;
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
    }
}
