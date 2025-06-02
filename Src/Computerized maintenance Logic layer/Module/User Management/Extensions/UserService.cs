using Computerized_maintenance_Logic_layer.Module.Tools;
using computrized_maintenance_Data_Access;

namespace Computerized_maintenance_Logic_layer.Module.User_Management.Extensions
{
    public class UserService
    {
        private UserService() { }
        public static int? GetPersonIdOfUser(int UserID)
        {
            return DataAccessUser.GetPersonID(UserID);
        }
        public static bool Reset_Password(int UserID,string NewPassword)
        {
            if ( !string.IsNullOrEmpty(NewPassword))
            {
                string HashValue = Security.HashEncrypt(NewPassword);
                return DataAccessUser.ResetPassword(UserID, HashValue);
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
