using Computerized_maintenance_Logic_layer.Module.Tools;
using computrized_maintenance_Data_Access.DTO;
using computrized_maintenance_Data_Access.UserManagement;

namespace Computerized_maintenance_Logic_layer.Module.User_Management
{
    public class ClsUsers
    {

        private readonly UserRepo _repository;

        /// <summary>
        /// constructor to init User class with person
        /// </summary>
        /// <param name="repository">an instance from User respository</param>
        public ClsUsers(UserRepo repository)
        {
            _repository = repository;
        }


        /// <summary>
        /// Retrieve single user data by user unique identifier
        /// </summary>
        /// <param name="UserID">unique identifier of user you looking for by it</param>
        /// <returns>data transfer object <see cref="UserDtoResponse"/> in case the operation has been succeed, otherwise <see langword="null"/></returns>
        public async Task<UserDtoResponse?> FindUserAsync (int UserID)
        {
            return await _repository.FindByIdAsync(UserID);
        }
        /// <summary>
        /// Retrieve single user data by UserName
        /// </summary>
        /// <param name="UserName">name of user you looking for by it</param>
        /// <returns>data transfer object <see cref="UserDtoResponse"/> in case the operation has been succeed, otherwise <see langword="null"/></returns>
        public async Task<UserDtoResponse?> FindUserAsync(string UserName)
        {
            return await _repository.FindByUserNameAsync(UserName);
        }


        public async Task<UserClaimDto?> BringUserWithIdAsync(string UserName)
        {
            return await _repository.getUserCliamDetails(UserName);
        }


        /// <summary>
        ///  Try out to add new user on system
        /// </summary>
        /// <param name="UserResquest"> data transfer object to <see cref="UserRequestDto"/></param>
        /// <returns><see langword="true"/> in case the save data on database has been done,<see langword="false"/> otherwise</returns>
        public async Task<bool> AddNewUserAsync(ProcessAddUserDto UserResquest)
        {
            if (UserResquest is not ProcessAddUserDto) return false;
            UserResquest.Password = Security.HashEncrypt(UserResquest.Password);

            return await _repository.AddNewUserAsync(UserResquest) > 0;
        }


        /// <summary>
        ///  Try out to Update user on system
        /// </summary>
        /// <param name="UserResquest"> data transfer object to <see cref="UserRequestDto"/></param>
        /// <returns><see langword="true"/> in case the Update data on database has been done,<see langword="false"/> otherwise</returns>
        public async Task<bool> UpdateUserAsync(ProcessUpdateUserDto UserResquest)
        {
            if(UserResquest is not ProcessUpdateUserDto) return false;
            UserResquest.userDto.Password = Security.HashEncrypt(UserResquest.userDto.Password);

            return await _repository.UpdateUserAsync(UserResquest);   
        }


        /// <summary>
        ///  Try out to delete a user on system
        /// </summary>
        /// <param name="UserID"> unique identifier of user </param>
        /// <param name="PersonID"> unique identifier of Person </param>
        /// <returns><see langword="true"/> in case the save data on database has been done,<see langword="false"/> otherwise</returns>
        public async Task<bool> DeleteUserAsync(int UserID)
        {
            return await _repository.DeleteUserAsync(UserID);
        }


        /// <summary>
        /// checking about User Exist on system or not 
        /// </summary>
        /// <param name="Userid">unique identifier of user</param>
        /// <returns><see langword="true"/> in case user exists on database,<see langword="false"/> otherwise</returns>
        public  async Task<bool> IsExistUserAsync(int UserId)
        {
            return await _repository.IsExistUserAsync(UserId);
        }

        /// <summary>
        /// Retrieve all users data form dataset
        /// </summary>
        /// <returns>List of <see cref="UserTableViewDto"/> , otherwise <see langword="null"/></returns>
        public async Task<IEnumerable<UserTableViewDto>?> GetAllUsersAsync()
        {
            return await _repository.GetAllUsersAsync();
        }

    }
}
