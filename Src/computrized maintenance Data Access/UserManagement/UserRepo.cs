using computrized_maintenance_Data_Access.DTO;
using computrized_maintenance_Data_Access.Misc;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace computrized_maintenance_Data_Access.UserManagement
{
    public sealed class UserRepo
    {
        //CRUD Opration Start

        public async Task<UserDtoResponse?> FindByIdAsync(int UserId)
        {
            if (UserId < 1) return null;

            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {

                    DynamicParameters UserParam = new DynamicParameters();
                    UserParam.Add("@UserID", UserId,DbType.Int32,ParameterDirection.Input);

                    connection.Open();

                    string sql = @"select * from dbo.GetUserView(@UserID)";
                    var result = await connection.QueryFirstAsync<UserDtoResponse>(sql, UserParam, commandType: CommandType.Text);

                    if (result is not null)
                    {
                        return result;
                    }

                    connection.Close();
                }
                catch (SqlException ex)
                {
                    //login Exception error
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            return null;
        }
        public  async Task<UserDtoResponse?> FindByUserNameAsync(string UserName)
        {
            if (string.IsNullOrEmpty(UserName)) return null;

            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {

                    DynamicParameters UserParam = new DynamicParameters();
                    UserParam.Add("@UserName", UserName);

                    connection.Open();

                    string sql = @"select * from Users where UserName = @UserName";
                    var result = await connection.QueryFirstAsync<UserDtoResponse>(sql, UserParam, commandType: CommandType.Text);

                    if (result is not null)
                    {
                        return result;
                    }

                    connection.Close();
                }
                catch (SqlException ex)
                {
                    //login Exception error
                    Console.WriteLine(ex.Message);
                   return null ;
                }
            }
            return null;
        }

        public async Task<UserClaimDto?> getUserCliamDetails(string UserName)
        {
            if (string.IsNullOrEmpty(UserName)) return null;
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters UserParam = new DynamicParameters();
                    UserParam.Add("@UserName", UserName);

                    connection.Open();
                    var result = await connection.QueryFirstAsync<UserClaimDto>("dbo.sp_UserCliamDetails", UserParam, commandType: CommandType.Text);

                    if (result is not null)
                    {
                        return result;
                    }
                    connection.Close();
                }
                catch (SqlException ex)
                {
                    //login Exception error
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            return null;
        }

        public static  async Task<int> Static_AddNewUserAsync(ProcessAddUserDto userDto)
        {
            if (userDto == null) return -1;

            int UserID = -1;
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters UserParam = new DynamicParameters();
                    UserParam.Add("@UserID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    UserParam.Add("@UserName", userDto.UserName);
                    UserParam.Add("@Password", userDto.Password);
                    UserParam.Add("@FirstName", userDto.FirstName);
                    UserParam.Add("@LastName", userDto.LastName);
                    UserParam.Add("@Birthday", userDto.BirthDay);
                    UserParam.Add("@Phone", userDto.Phone);
                    UserParam.Add("@Email", userDto.Email);
                    UserParam.Add("@Address", userDto.Address);
                    UserParam.Add("@RoleName", userDto.RoleName);
                    UserParam.Add("@Permission", userDto.Permission);
                    UserParam.Add("@IsActive", userDto.IsActive);
                    UserParam.Add("@CreateAt", userDto.CreateAt);

                    connection.Open();
                    if ( await connection.ExecuteAsync("[dbo].{Sp_AddNewUser]", UserParam, commandType: CommandType.StoredProcedure) > 0)
                    {
                        UserID = UserParam.Get<int>("@UserID");
                    }

                    connection.Close();

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
            }

            return UserID;
        }
        public  async Task<int> AddNewUserAsync(ProcessAddUserDto userDto) => await Static_AddNewUserAsync(userDto);

        public static async Task<bool> Static_UpdateUserAsync(ProcessUpdateUserDto userDto)
        {
            if (userDto == null) return false;

            bool IsUpdateSuccess = false;
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters UserParam = new DynamicParameters();
                    UserParam.Add("@UserID", userDto.UserId);
                    UserParam.Add("@UserName", userDto.userDto.UserName);
                    UserParam.Add("@Password", userDto.userDto.Password);
                    UserParam.Add("@FirstName", userDto.userDto.FirstName);
                    UserParam.Add("@LastName", userDto.userDto.LastName);
                    UserParam.Add("@Birthday", userDto.userDto.BirthDay);
                    UserParam.Add("@Phone", userDto.userDto.Phone);
                    UserParam.Add("@Email", userDto.userDto.Email);
                    UserParam.Add("@Address", userDto.userDto.Address);
                    UserParam.Add("@Rolename", userDto.userDto.RoleName);
                    UserParam.Add("@Permission", userDto.userDto.Permission);
                    UserParam.Add("@IsActive", userDto.userDto.IsActive);
                    UserParam.Add("@CreateAt", userDto.userDto.CreateAt);

                    connection.Open();
                    IsUpdateSuccess = await connection.ExecuteAsync("[dbo].[Sp_UpdateUser]", UserParam, commandType: CommandType.StoredProcedure) > 0;
                    connection.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return IsUpdateSuccess;
            }
        }

        public  async Task<bool> UpdateUserAsync(ProcessUpdateUserDto userDto)
        {
            return await Static_UpdateUserAsync(userDto);
        }
        public  async Task<bool> DeleteUserAsync(int UserID)
        {
            if (UserID < 1) return false;
            //if (PersonID < 1) return false;

            bool IsDeleteSuccess = false;

            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters UserParam = new DynamicParameters();
                    UserParam.Add("@UserID", UserID);

                    connection.Open();
                    IsDeleteSuccess = await connection.ExecuteAsync("[dbo].[Sp_DeleteUser]", UserParam, commandType: CommandType.StoredProcedure) > 0;
                    connection.Close ();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return IsDeleteSuccess;
        }

        public  async Task<IEnumerable<UserTableViewDto>?> GetAllUsersAsync()
        {

            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {

                    string queyr = @"select * from GetAll_Users()";

                    var Result = await connection.QueryAsync<UserTableViewDto>(queyr, commandType: CommandType.Text);

                    if (Result != null)
                    {
                        return Result.ToList();
                    }

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }

            return null;
        }

        public async Task<bool> IsExistUserAsync(int UserID)
        {
            if (UserID < 1) return false;

            bool IsExist = false;
            try
            {

                using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
                {
                    var UserParam = new DynamicParameters();

                    UserParam.Add(name: "@UserID", value: UserID, dbType: DbType.Int32, direction: ParameterDirection.Input);

                    string query = "SELECT Find= 1 FROM Users WHERE UserID =@UserID";

                    connection.Open();

                    IsExist =  await connection.ExecuteScalarAsync<byte>(query, param: UserParam, commandType: CommandType.Text) > 0;

                    connection.Close();
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error Database : {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Server : {ex.Message}");
                //add object To Save Loging on Data Base to catch her in future
                return false;
            }

            return IsExist;
        }

        public static async Task<int?> GetPersonIdAsync(int UserID)
        {
            if ( UserID < 1) return null;

            int? PersonID = null;
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters Userparam = new DynamicParameters();
                    Userparam.Add("@UserID", UserID);
                    Userparam.Add("@PersonID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    Userparam.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    connection.Open();
                    await connection.ExecuteAsync("dbo.Sp_GetPesonIDFromUsers", Userparam, commandType: CommandType.StoredProcedure);

                    if (Userparam.Get<int>("ReturnValue") > 0)
                    {
                        PersonID = Userparam.Get<int>("@PersonID");
                    }

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
            }
            return PersonID;
        }

        public async Task<bool> ResetPasswordAsync(int UserId,string NewPassword)
        {
            if (UserId < 1) return false;
            if(string.IsNullOrEmpty(NewPassword)) return false;

            bool IsRested = false;
            try
            {
                using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
                {

                    var userParam = new DynamicParameters();
                    userParam.Add("@UserID", UserId);
                    userParam.Add("@Password", NewPassword);

                    connection.Open();
                    IsRested = await connection.ExecuteAsync("Sp_ResetPassword", userParam, commandType: CommandType.StoredProcedure) > 0;
                    connection.Close();
                }

            }
            catch (SqlException Sx)
            {
                Console.WriteLine(Sx.Message);
                return false;
            }

            return IsRested;
        }

        private async Task<bool> VerifyLoginAsync(string UserName, string Password)
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password)) return false;

            bool IsValidAccount = false;
            try
            {

                using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
                {
                    var userParam = new DynamicParameters();
                    userParam.Add("@Username", UserName);
                    userParam.Add("@Password", Password);

                    connection.Open();

                    IsValidAccount = await connection.ExecuteScalarAsync<byte>("Sp_Verify_Exist_User_Login"
                        , param: userParam, commandType: CommandType.StoredProcedure) == 1;

                    connection.Close();
                }

            }
            catch (SqlException Sx)
            {
                Console.WriteLine(Sx.Message);
                return false;
            }

            return IsValidAccount;
        }

        public  async Task<bool> IsUserNameAndPasswordValidAsync(string UserName , string Password)
        {
           return await VerifyLoginAsync(UserName, Password);
        }
    }
}
