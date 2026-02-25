using Computerized_maintenance_Logic_layer.Module.Tools;
using computrized_maintenance_Data_Access.UserManagement;

namespace Computerized_maintenance_Logic_layer.Module.User_Management.Extensions
{
    public class UserService
    {
        private readonly UserRepo _UserRepo;

        public UserService(UserRepo userRepo)
        {
            _UserRepo = userRepo;
        }

        /// <summary>
        /// Retrieve person identifier associated with a specific user
        /// </summary>
        /// <param name="UserID"> unique identifier of user</param>
        /// <returns><see langword="int"/> in case operation has been done, otherwise <see langword="null"/></returns>
        public async Task<int?> GetPersonIdOfUserAsync(int UserID)
        {
            if (UserID < 1) return null;

            return await UserRepo.GetPersonIdAsync(UserID);
        }

        /// <summary>
        /// Reset password for specific user
        /// </summary>
        /// <param name="UserID"> unique identifier of user</param>
        /// <param name="NewPassword">new password to change it</param>
        /// <returns><see langword="true"/> in case operation has been done, otherwise <see langword="false"/></returns>
        public async Task<bool> Reset_PasswordAsync(int UserID, string NewPassword)
        {
            if (UserID > 0 && !string.IsNullOrEmpty(NewPassword))
            {
                string HashValue = Security.HashEncrypt(NewPassword);

                return await _UserRepo.ResetPasswordAsync(UserID, HashValue);
            }

            return false;
        }


        /// <summary>
        /// Verify user Login by username and password is corrected or not
        /// </summary>
        /// <param name="Username">name of user on the system</param>
        /// <param name="Password">Real password of user </param>
        /// <returns><see langword="true"/> in case the operation has been verified, otherwise <see langword="false"/></returns>
        public async Task<bool> Verfiy_User_LoginAsync(string? Username, string? Password)
        {
            return await Check_Validation_UserName_And_PasswordAsync(Username!, Password!);
        }

        /// <summary>
        /// Verify user Login by username and password is corrected or not
        /// </summary>
        /// <param name="Username">name of user on the system</param>
        /// <param name="Password">Real password of user </param>
        /// <returns><see langword="true"/> in case the operation has been verified, otherwise <see langword="false"/></returns>
        public async Task<bool> Check_Validation_UserName_And_PasswordAsync(string Username, string Password)
        {
            if (!string.IsNullOrEmpty(Username) || !string.IsNullOrEmpty(Password))
            {
                string hash_Password = Security.HashEncrypt(Password!);

                return await _UserRepo.IsUserNameAndPasswordValidAsync(Username!, hash_Password);
            }

            return false;
        }
    }
}
