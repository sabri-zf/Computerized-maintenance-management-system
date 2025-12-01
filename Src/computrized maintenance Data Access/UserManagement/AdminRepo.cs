using computrized_maintenance_Data_Access.DTO;
using computrized_maintenance_Data_Access.Misc;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using System.Security;
using System.Transactions;

namespace computrized_maintenance_Data_Access.UserManagement
{
    public sealed class AdminRepo
    {
        public  async Task<AdminDtoResponse?> FinsByIdAsync(int ID)
        {
            if (ID < 1) return null;

            using (IDbConnection connection = new SqlConnection(connectionString: ClsUtility.ConnectionString))
            {
                try
                {

                    DynamicParameters AdminIDparam = new DynamicParameters();
                    AdminIDparam.Add("@AdminID", ID);

                    connection.Open();

                    var Result = await connection.QueryFirstAsync<AdminDtoResponse>("dbo.sp_FindAdminWithDetails", AdminIDparam, commandType: CommandType.StoredProcedure);
                                           

                    if (Result is AdminDtoResponse)
                    {
                        return Result;
                    }

                    connection.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }

            return null;
        }
        public  async Task<int> AddNewAdminAsync(ProcessAddUserDto admin)
        {
            if (admin == null) return -1;
           
            if(string.IsNullOrEmpty(admin.UserName)) return -1;
            if(string.IsNullOrEmpty(admin.Password)) return -1;
            if(string.IsNullOrEmpty(admin.FirstName)) return -1;
            if(string.IsNullOrEmpty(admin.LastName)) return -1;
            if(string.IsNullOrEmpty(admin.RoleName)) return -1;
            if(string.IsNullOrEmpty(admin.Address)) return -1;
            if(string.IsNullOrEmpty(admin.Email)) return -1;
            if(string.IsNullOrEmpty(admin.Phone)) return -1;
            if(admin.Permission < 0) return -1;
            if (admin.CreateAt.CompareTo(DateTime.Now) > 0) return -1;
            

            int PersonId = -1;

            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {

                    DynamicParameters AdminParam = new DynamicParameters();

                    AdminParam.Add("@Username", admin.UserName,DbType.String,ParameterDirection.Input);
                    AdminParam.Add("@Password", admin.Password, DbType.String, ParameterDirection.Input);
                    AdminParam.Add("@Firstname", admin.FirstName, DbType.String, ParameterDirection.Input);
                    AdminParam.Add("@Lastname", admin.LastName, DbType.String, ParameterDirection.Input);
                    AdminParam.Add("@Birthday", admin.BirthDay, DbType.DateTime2, ParameterDirection.Input);
                    AdminParam.Add("@PhoneNumber", admin.Phone, DbType.String, ParameterDirection.Input);
                    AdminParam.Add("@Email", admin.Email, DbType.String, ParameterDirection.Input);
                    AdminParam.Add("@Address", admin.Address, DbType.String, ParameterDirection.Input);
                    AdminParam.Add("@Rolename", admin.RoleName, DbType.String, ParameterDirection.Input);
                    AdminParam.Add("@Permission", admin.Permission, DbType.Int16, ParameterDirection.Input);
                    AdminParam.Add("@IsActive", admin.IsActive, DbType.Boolean, ParameterDirection.Input);
                    AdminParam.Add("@CreateAt", admin.CreateAt, DbType.DateTime2, ParameterDirection.Input);
                    AdminParam.Add("@AdminID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    connection.Open();

                   await connection.ExecuteAsync("dbo.Sp_AddNewAdmin", param: AdminParam, commandType: CommandType.StoredProcedure);

                    PersonId = AdminParam.Get<int>("@AdminID");

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            return PersonId;
        }
        public  async Task<bool> DeleteAdminAsync(int AdminID)
        {
            if (AdminID < 1) return false;

            bool IsDeleted = false;

            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters AdminParam = new DynamicParameters();
                    AdminParam.Add("@AdminID", AdminID, DbType.Int32, ParameterDirection.Input);

                    connection.Open();

                    IsDeleted =  await connection.ExecuteAsync("[dbo].[sp_Remove_AdminByID]", AdminParam, commandType: CommandType.StoredProcedure) > 0;

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    IsDeleted = false;
                }
            }

            return IsDeleted;
        }
        public  async Task<bool> UpdateAdminAsync(ProcessUpdateAdminDto updateAdminDto)
        {
            if (updateAdminDto == null) return false;
            if (updateAdminDto.AdminID < 1) return false;
            if (string.IsNullOrEmpty(updateAdminDto.UserName)) return false;
            if (string.IsNullOrEmpty(updateAdminDto.Password)) return false;
            if (string.IsNullOrEmpty(updateAdminDto.FirstName)) return false;
            if (string.IsNullOrEmpty(updateAdminDto.LastName)) return false;
            if (string.IsNullOrEmpty(updateAdminDto.RoleName)) return false;
            if (string.IsNullOrEmpty(updateAdminDto.Address)) return false;
            if (string.IsNullOrEmpty(updateAdminDto.Email)) return false;
            if (string.IsNullOrEmpty(updateAdminDto.Phone)) return false;
            if (updateAdminDto.Permission < 0) return false;
            if (updateAdminDto.CreateAt.CompareTo(DateTime.Now) > 0) return false;


            bool IsUpdated = false;
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters AdminParam = new DynamicParameters();

                    AdminParam.Add("@AdminID", updateAdminDto.AdminID, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    AdminParam.Add("@Username", updateAdminDto.UserName, DbType.String, ParameterDirection.Input);
                    AdminParam.Add("@Password", updateAdminDto.Password, DbType.String, ParameterDirection.Input);
                    AdminParam.Add("@Firstname", updateAdminDto.FirstName, DbType.String, ParameterDirection.Input);
                    AdminParam.Add("@Lastname", updateAdminDto.LastName, DbType.String, ParameterDirection.Input);
                    AdminParam.Add("@Birthday", updateAdminDto.BirthDay, DbType.DateTime2, ParameterDirection.Input);
                    AdminParam.Add("@PhoneNumber", updateAdminDto.Phone, DbType.String, ParameterDirection.Input);
                    AdminParam.Add("@Email", updateAdminDto.Email, DbType.String, ParameterDirection.Input);
                    AdminParam.Add("@Address", updateAdminDto.Address, DbType.String, ParameterDirection.Input);
                    AdminParam.Add("@Rolename", updateAdminDto.RoleName, DbType.String, ParameterDirection.Input);
                    AdminParam.Add("@Permission", updateAdminDto.Permission, DbType.Int16, ParameterDirection.Input);
                    AdminParam.Add("@IsActive", updateAdminDto.IsActive, DbType.Boolean, ParameterDirection.Input);
                    AdminParam.Add("@CreateAt", updateAdminDto.CreateAt, DbType.DateTime2, ParameterDirection.Input);

                    connection.Open();
                    IsUpdated = await connection.ExecuteAsync("dbo.UpdateAdmin", AdminParam, commandType: CommandType.StoredProcedure) > 0;
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    IsUpdated = false;
                }
            }
            return IsUpdated;
        }
        public  async Task<bool> IsExistAdminAsync(int ID)
        {
            if (ID < 1) return false;
            bool IsExist = false;
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters AdminParam = new DynamicParameters();
                    AdminParam.Add("@AdminID", ID);
                    connection.Open();
                    string querey = @"select Found = 1 from Admins where AdminID = @AdminID";
                    var Result = await connection.ExecuteScalarAsync(querey, AdminParam, commandType: CommandType.Text);
                    IsExist = (Result != null && (int)Result > 0);
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    IsExist = false;
                }
            }
            return IsExist;
        }
        public  async Task<IEnumerable<AdminTableViewDto>?> GetAllAdminsAsync()
        {
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    string querey = @"select * From GetAll_Adimns()";
                    connection.Open();
                    var Result = await connection.QueryAsync<AdminTableViewDto>(querey, null, commandType: CommandType.Text);
                    if (Result != null)
                    {
                        connection.Close();
                        return Result;
                    }
                }
                catch
                (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return null;
        }

    }
}
