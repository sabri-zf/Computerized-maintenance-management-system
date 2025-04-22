using Microsoft.Extensions.Configuration;

namespace CMMS_Api.Helper
{
    public class JwtMapping
    {
        public string Issuer { get;set; }
        public string Audience { get; set; }
        public byte LifeTime { get; set; }
        public string IssuerSigningKey { get; set; }

        // using Si
        private static JwtMapping _Instance;

        public static JwtMapping? Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .Build()
                        .GetSection("JWT")
                        .Get<JwtMapping>()!;
                }

                return _Instance;
            }
        }


        ~JwtMapping()
        {
            GC.SuppressFinalize(_Instance);
        }
    }
}
