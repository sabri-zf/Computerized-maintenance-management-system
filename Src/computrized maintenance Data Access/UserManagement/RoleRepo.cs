using computrized_maintenance_Data_Access.DTO;
using computrized_maintenance_Data_Access.Misc;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace computrized_maintenance_Data_Access.UserManagement
{
    public sealed class RoleRepo
    {

        /// <summary>
        /// retrive Role Name via unique identitfier role id
        /// </summary>
        /// <param name="Id"> unique identitfier of role id</param>
        /// <returns>data transfate object <see cref="RoleDtoResponse"/>, otherwise <see langword="null"/></returns>
        public async Task<RoleDtoResponse?> FindByIdAsync(int Id)
        {

            if (Id < 1) return null;

            using (IDbConnection Connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {

                    DynamicParameters RoleParam = new DynamicParameters();
                    RoleParam.Add("@RoleID", Id);


                    Connection.Open();
                    RoleDtoResponse? Result = await Connection.QueryFirstAsync<RoleDtoResponse>("[dbo].[Sp_GetRoleById]", RoleParam,commandType: CommandType.StoredProcedure);
                    Connection.Close();

                    if (Result != null)
                    {
                        return Result;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);

                     return null;
                }
            }
            return null;
        }

        /// <summary>
        /// Retrive whole Role Name from Data store asynchronously
        /// </summary>
        /// <returns>a list has data transfer object <see cref="RoleDtoResponse"/>, otherwise <see langword="null"/> </returns>
        public async Task<IEnumerable<RoleDtoResponse>?> RetrieveWholeRolesAsync()
        {
            //?List<RoleDtoRequest> List = new List<RoleDtoRequest>();

            using (IDbConnection Connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {

                    var Result = await Connection.QueryAsync<RoleDtoResponse>("[dbo].[sp_GetAllRoles]", commandType:CommandType.StoredProcedure);

                    if (Result != null)
                    {
                        return Result;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null; 
                }
            }
                return null;
        }

        /// <summary>
        /// Add new value of RoleName on the dataset asynchronously
        /// </summary>
        /// <param name="roleName">new Role name to add it</param>
        /// <returns><see langword="true"/> if the operation has been succeed, otherwise <see langword="false"/></returns>
        public async Task<bool> AddNewRoleAsync(string roleName)
        {
            if (string.IsNullOrEmpty(roleName)) return false;

            bool isAdded = false;
            using (IDbConnection Connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters RoleParameter = new DynamicParameters();
                    RoleParameter.Add("@RoleName", roleName.Trim(), DbType.String, ParameterDirection.Input);

                    Connection.Open();
                    isAdded = await Connection.ExecuteAsync("[dbo].[sp_AddNewRole]", RoleParameter,commandType: CommandType.StoredProcedure) > 0;
                    Connection.Close();

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);

                    return false;
                }

                return isAdded;
            }
        }

        /// <summary>
        /// Modify Role name on dataset asynchronously
        /// </summary>
        /// <param name="OldRoleName">the old version of role name, it was existing</param>
        /// <param name="NewRoleName">the new version of role name, made by user</param>
        /// <returns><see langword="true"/> if the operation has been succeed, otherwise <see langword="false"/></returns>
        public async Task<bool> UpdateRoleAsync(string OldRoleName, string NewRoleName)
        {
            if (string.IsNullOrEmpty(OldRoleName)) return false;
            if (string.IsNullOrEmpty(NewRoleName)) return false;

            bool IsUpdated = false;
            using (IDbConnection Connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters RoleParameter = new ();
                    RoleParameter.Add("@OldRoleName",OldRoleName, DbType.String, ParameterDirection.Input);
                    RoleParameter.Add("@NewRoleName",NewRoleName, DbType.String, ParameterDirection.Input);

                    Connection.Open();
                    IsUpdated = await Connection.ExecuteAsync("[dbo].[sp_UpdateRole]", RoleParameter, commandType: CommandType.StoredProcedure) > 0;
                    Connection.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }

                return IsUpdated;
            }
        }

        /// <summary>
        /// Delete recored Role on dataset asynchronously
        /// </summary>
        /// <param name="RoleName">represent the name of role exist on dataset</param>
        /// <returns><see langword="true"/> if the operation has been succeed, otherwise <see langword="false"/></returns>
        public async Task<bool> DeleteRoleAsync(string RoleName)
        {
            if(string.IsNullOrEmpty(RoleName)) return false;

            var IsDeleted = false;
            using(IDbConnection Connection = new SqlConnection(ClsUtility.ConnectionString))
            {

                try
                {
                    DynamicParameters RoleParam = new();
                    RoleParam.Add("@RoleName", RoleName, DbType.String, ParameterDirection.Input);

                    Connection.Open();
                    IsDeleted = await Connection.ExecuteAsync("[dbo].[sp_DeleteRole]", RoleParam, commandType: CommandType.StoredProcedure) > 0;
                    Connection.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }


                return IsDeleted;
            }
        }
    
    
    }
}
