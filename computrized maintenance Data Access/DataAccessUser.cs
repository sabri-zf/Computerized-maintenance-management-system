using computrized_maintenance_Data_Access.DTO;
using computrized_maintenance_Data_Access.Misc;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computrized_maintenance_Data_Access
{
    public  class DataAccessUser
    {


        public static bool Find(int? UserID,ref UserDto dto)
        {
            if (UserID < 0) return false;

            bool IsFound = false;
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {

                    DynamicParameters UserParam = new DynamicParameters();
                    UserParam.Add("@UserID", UserID);

                    connection.Open();

                    string sql = @"select * from Users where UserID = @UserID";
                    var result = connection.Query<UserDto>(sql,UserParam,commandType: CommandType.Text).SingleOrDefault();

                    if (result != null)
                    {
                        dto = result;
                        IsFound = true;
                    }


                }
                catch (SqlException ex)
                {
                    //login Exception error
                    Console.WriteLine(ex.Message);
                    IsFound =  false;
                }
            }
            return IsFound;
        }

        public static int AddNewUser(UserDto userDto)
        {
            if (userDto == null) return -1;

            int PersonID = -1;
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters UserParam = new DynamicParameters();
                    UserParam.Add("@UserID",dbType:DbType.Int32,direction:ParameterDirection.Output);
                    UserParam.Add("@UserName",userDto.UserName);
                    // Hashing password
                    UserParam.Add("@Password",userDto.Password);
                    UserParam.Add("@PersonID",userDto.personID);
                    UserParam.Add("@RoleID",userDto.RoleID);
                    UserParam.Add("@Permission",userDto.permission);
                    UserParam.Add("@IsActive",userDto.IsActive);
                    UserParam.Add("@CreateAt",userDto.createAt);

                    connection.Open();
                    if (connection.Execute("Sp_AddNewUser", UserParam, commandType: CommandType.StoredProcedure) > 0)
                    {
                        PersonID = UserParam.Get<int>("@UserID");
                    }


                    }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

            return PersonID;
        }


        public static bool UpdateUser(UserDto userDto)
        {
            if (userDto == null ) return false;

            bool IsUpdateSuccess = false;
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters UserParam = new DynamicParameters();
                    UserParam.Add("@UserID",userDto.UserID);
                    UserParam.Add("@UserName", userDto.UserName);
                    // Hashing password
                    UserParam.Add("@Password", userDto.Password);
                    UserParam.Add("@RoleID", userDto.RoleID);
                    UserParam.Add("@Permission", userDto.permission);
                    UserParam.Add("@IsActive", userDto.IsActive);

                    connection.Open ();
                    IsUpdateSuccess = connection.Execute("Sp_UpdateUser", UserParam, commandType: CommandType.StoredProcedure) > 0;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }

                return IsUpdateSuccess;
            }
        }

        public static bool DeleteUser(int? UserID,int? PersonID)
        {
         
            if (!UserID.HasValue || UserID < 1) return false;
            if (!PersonID.HasValue || PersonID < 1) return false;

            bool IsDeleteSuccess = false;

            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters UserParam = new DynamicParameters();
                    UserParam.Add("@UserID", UserID);
                    UserParam.Add("@PersonID", PersonID);

                    connection.Open ();
                    IsDeleteSuccess = connection.Execute("Sp_DeleteUser", UserParam, commandType: CommandType.StoredProcedure) > 0;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            return IsDeleteSuccess;
        }

        public static List<UserViewDto> GetAllUsers()
        {

            List<UserViewDto > ListUsers = new List<UserViewDto>();
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {

                    string queyr = @"select * from GetAll_Users()";

                    var Result = connection.Query<UserViewDto>(queyr, commandType: CommandType.Text);

                    if (Result != null)
                    {
                        ListUsers = Result.ToList();
                    }

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

            return ListUsers;
        }

        public static int? GetPersonID(int? UserID)
        {
            if (UserID == null || UserID < 1) return null;

            int? PersonID = null;
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters Userparam = new DynamicParameters();
                    Userparam.Add("@UserID", UserID);
                    Userparam.Add("@PersonID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    if (connection.Execute("Sp_GetPesonIDFromUsers", Userparam, commandType: CommandType.StoredProcedure) > 0)
                    {
                        PersonID = Userparam.Get<int>("@PersonID");
                    };



                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            return PersonID;
        }

    }
}
