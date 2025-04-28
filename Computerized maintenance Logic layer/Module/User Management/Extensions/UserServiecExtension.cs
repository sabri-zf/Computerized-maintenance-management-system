using Computerized_maintenance_Logic_layer.Module.Tools;
using computrized_maintenance_Data_Access;

namespace Computerized_maintenance_Logic_layer.Module.User_Management.Extensions
{
    public static class UserServiecExtension
    {
        public static bool Reset_Password(this ClsUsers? User, string NewPassword)
        {
            if (User != null || !string.IsNullOrEmpty(NewPassword))
            {
                string HashValue = Security.HashEncrypt(NewPassword);
                return DataAccessUser.ResetPassword(User!.UserID, HashValue);
            }

            return false;
        }

        public static bool Verfiy_User_Login(string Username, string Password)
        {
            if (!string.IsNullOrEmpty(Username) || !string.IsNullOrEmpty(Password))
            {
                return DataAccessUser.VerifyLogin(Username, Password);
            }

            return false;
        }

        public static bool Check_Validation_UserName_And_Password(string Username, string Password)
        {
            return DataAccessUser.IsUserNameAndPasswordValid(Username, Password);
        }
    }
}
