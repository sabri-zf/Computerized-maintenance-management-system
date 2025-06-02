using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computrized_maintenance_Data_Access.Helper
{
    public class Configure
    {
        public IConfiguration configure {  get; set; }

        private static object _Loack = new object();

        private static Configure _Instance;

        public static Configure? Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_Loack)
                    {
                        if (_Instance is null)
                        {
                            return _Instance = new Configure("AppSetting.json");
                        }

                    }
                }
                    return _Instance;
            }
        }

        private Configure(string FileConfiguration)
        {
          configure = new ConfigurationBuilder().AddJsonFile(FileConfiguration).Build();

            if (configure is null)
            {
                throw new ArgumentNullException("Exception ouccor in " + nameof(Configure) + " Class");
            }
        }


        public string? ConfigurationObtainSection(string value)
        {
            return configure.GetSection(value).Value;
        }
    }
}
