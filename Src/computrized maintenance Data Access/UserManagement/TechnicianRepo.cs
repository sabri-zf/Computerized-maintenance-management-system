using computrized_maintenance_Data_Access.DTO;
using computrized_maintenance_Data_Access.Misc;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace computrized_maintenance_Data_Access.UserManagement
{
    public sealed class TechnicianRepo
    {

        public async Task<TechnicianDtoRepose?> FindByIDAsync(int ID)
        {
            if (ID < 1) return null;

            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {

                    DynamicParameters TechnicianParam = new DynamicParameters();
                    TechnicianParam.Add("@TechnincianID", ID);

                    connection.Open();
                    var Result = await connection.QueryFirstAsync<TechnicianDtoRepose>("Sp_FindByTechnicianID", TechnicianParam, commandType: CommandType.StoredProcedure);
                    connection.Close();
                    if (Result != null) return Result;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }

            }

            return null;
        }

        public async Task<bool> AddNewTechnicianAsync(ProcessToCreateTechnicianDto dto)
        {
            if(dto == null) return false;
            if(string.IsNullOrEmpty(dto.UserName)) return false;
            if(string.IsNullOrEmpty(dto.Password)) return false;
            if(string.IsNullOrEmpty(dto.FirstName)) return false;
            if(string.IsNullOrEmpty(dto.LastName)) return false;
            if(string.IsNullOrEmpty(dto.Phone)) return false;
            if(string.IsNullOrEmpty(dto.Address)) return false;
            if(string.IsNullOrEmpty(dto.Email)) return false;
            if(string.IsNullOrEmpty(dto.RoleName)) return false;
            if(string.IsNullOrEmpty(dto.DepartmentName)) return false;
            if(string.IsNullOrEmpty(dto.ManagedBy)) return false;
            if(string.IsNullOrEmpty(dto.CreatedBy)) return false;
            if(dto.permission < 0) return false;

            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters TechParam = new DynamicParameters();
                    TechParam.Add("@UserName",dto.UserName);
                    TechParam.Add("@Password", dto.Password);
                    TechParam.Add("@FirstName", dto.FirstName);
                    TechParam.Add("@LastName", dto.LastName);
                    TechParam.Add("@Email", dto.Email);
                    TechParam.Add("@Phone", dto.Phone);
                    TechParam.Add("@Address", dto.Address);
                    TechParam.Add("@BirthDay", dto.BirthDay);
                    TechParam.Add("@RoleName", dto.RoleName);
                    TechParam.Add("@Permission", dto.permission);
                    TechParam.Add("@IsActive", dto.IsActive);
                    TechParam.Add("@CreateAt", dto.createAt);
                    TechParam.Add("@DepartmentName", dto.DepartmentName);
                    TechParam.Add("@ManagedBy", dto.ManagedBy);
                    TechParam.Add("@CreatedBy", dto.CreatedBy);
                    TechParam.Add("@TechnicianID",dbType:DbType.Int32,direction:ParameterDirection.Output);

                    connection.Open();
                  var IsAdded = await  connection.ExecuteAsync("Sp_AddNewTechnician",TechParam, commandType: CommandType.StoredProcedure) > 0;
                    connection.Close();
                   
                    if (IsAdded) return true;

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }

            return false;
        }

        public async  Task<bool> UpdateTechnicianAsync(ProcessToModifyTechnicianDto dto)
        {
            if (dto == null ) return false;

            if(dto.TechnicianID < 1) return false;
            if (string.IsNullOrEmpty(dto.UserName)) return false;
            if (string.IsNullOrEmpty(dto.Password)) return false;
            if (string.IsNullOrEmpty(dto.FirstName)) return false;
            if (string.IsNullOrEmpty(dto.LastName)) return false;
            if (string.IsNullOrEmpty(dto.Phone)) return false;
            if (string.IsNullOrEmpty(dto.Address)) return false;
            if (string.IsNullOrEmpty(dto.Email)) return false;
            if (string.IsNullOrEmpty(dto.RoleName)) return false;
            if (string.IsNullOrEmpty(dto.DepartmentName)) return false;
            if (string.IsNullOrEmpty(dto.ManagedBy)) return false;
            if (dto.permission < 0) return false;

        
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters TechParam = new DynamicParameters();
                    TechParam.Add("@TechnicianID", dto.TechnicianID);
                    TechParam.Add("@UserName", dto.UserName);
                    TechParam.Add("@Password", dto.Password);
                    TechParam.Add("@FirstName", dto.FirstName);
                    TechParam.Add("@LastName", dto.LastName);
                    TechParam.Add("@Email", dto.Email);
                    TechParam.Add("@Phone", dto.Phone);
                    TechParam.Add("@Address", dto.Address);
                    TechParam.Add("@BirthDay", dto.BirthDay);
                    TechParam.Add("@RoleName", dto.RoleName);
                    TechParam.Add("@Permission", dto.permission);
                    TechParam.Add("@IsActive", dto.IsActive);
                    TechParam.Add("@DepartmentName", dto.DepartmentName);
                    TechParam.Add("@ManagedBy", dto.ManagedBy);

                    connection.Open();
                     var IsUpdateted = await connection.ExecuteAsync("Sp_UpdateTechnician", TechParam, commandType: CommandType.StoredProcedure) > 0;
                    connection.Close();

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);

                    return false;
                }
            }
            return false;
        }

        public async Task<bool> DeleteTechnicianAsync(int ID)
        {
            if(ID< 1) return false;

            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {

                    var TechParam = new DynamicParameters();
                    TechParam.Add("@TechnicianID", ID);

                    connection.Open();
                 var HasBeenDeleted = await connection.ExecuteAsync("Sp_DeleteThechnician", TechParam, commandType: CommandType.StoredProcedure) > 0;

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }

                return false;
            }
        }


        public async  Task<IEnumerable<TechnicianTableViewDto>?> RetrieveAllTechnicionAsync()
        {
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    connection.Open();
                    var Result =  await connection.QueryAsync<TechnicianTableViewDto>("Sp_GetAllTechnicians", commandType: CommandType.StoredProcedure);
                    connection.Close();
                    if (Result != null) return Result;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            return null;
        }
    }
}
