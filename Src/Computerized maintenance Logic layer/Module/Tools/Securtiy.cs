using System.Security.Cryptography;
using System.Text;

namespace Computerized_maintenance_Logic_layer.Module.Tools
{
    public class Security
    {
        public static string HashEncrypt(string Plant)
        {
            using (SHA256 sha = SHA256.Create())
            {

                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(Plant));

                return Convert.ToBase64String(bytes);
            }
        }
    }
}
