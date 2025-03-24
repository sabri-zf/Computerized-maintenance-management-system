using computrized_maintenance_Data_Access.DTO;
using computrized_maintenance_Data_Access.Misc;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computrized_maintenance_Data_Access
{
    public class DataAccessManager
    {
        public static bool Find(int? ID , ref ManagerDto dto)
        {
            if (ID < 1 || ID is null) return false;

            bool IsFound = false;
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {

                try
                {
                    DynamicParameters ManagerParam = new DynamicParameters();
                    ManagerParam.Add("@ManagerID", ID);

                    var Result = connection.Query<ManagerDto>("Sp_FindManagerByID", ManagerParam, commandType: CommandType.StoredProcedure).SingleOrDefault();

                    if (Result != null)
                    {
                        IsFound = true;
                        dto = Result;
                    }
                }
                catch (Exception ex)
                {
                    IsFound = false;
                    Console.WriteLine(ex);
                    throw;
                }
            }
            return IsFound;
        }

        public static int? AddNewManager(ManagerDto dto)
        {
            if (dto == null) return null;

            int? ManagerId = null;
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters ManagerParam = new DynamicParameters();
                    ManagerParam.Add("@UserID",dto.UserID);
                    ManagerParam.Add("@DepartmentID",dto.DepartmentID);
                    ManagerParam.Add("@ManagedBy",dto.ManagedBy);
                    ManagerParam.Add("@CreatedByAdmin",dto.CreatedByAdmin);
                    ManagerParam.Add("@ManagerID",dbType:DbType.Int32,direction:ParameterDirection.Output);


                    var RowEffected = connection.Execute("Sp_AddNewManager",param: ManagerParam,commandType:CommandType.StoredProcedure);

                    if (RowEffected > 0)
                    {
                        ManagerId = ManagerParam.Get<int>("@ManagerID");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                   throw;
                }
            }
            return ManagerId;
        }

        public static bool UpdateManger(ManagerDto dto)
        {
            if (dto == null) return false;

            bool IsUpdateManager = false;
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters ManagerParam = new DynamicParameters();
                    ManagerParam.Add("@ManagerID", dto.ManagerID);
                    ManagerParam.Add("@DepartmentID", dto.DepartmentID);
                    ManagerParam.Add("@ManagedBy", dto.ManagedBy);

                    connection.Open();

                    IsUpdateManager = connection.Execute("Sp_UpdateManager",ManagerParam,commandType:CommandType.StoredProcedure) > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            return IsUpdateManager;
        }

        public static bool DeleteManager(ManagerDto managerDto)
        {
            if(managerDto == null) return false;

            bool IsDeletedManager = false;
            using (IDbConnection connection = new SqlConnection())
            {
                try
                {
                    int? personID = DataAccessUser.GetPersonID(managerDto.UserID);

                    if (personID == null) return false;

                    DynamicParameters Managerparam = new DynamicParameters();
                    Managerparam.Add("@ManagerID", managerDto.ManagerID);
                    Managerparam.Add("@ManagerID", managerDto.UserID);
                    Managerparam.Add("@PersonID", personID);

                    connection.Open();
                   IsDeletedManager = connection.Execute("Sp_DeleteManager", param:Managerparam,commandType:CommandType.StoredProcedure) > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

            return IsDeletedManager;
        }

        public static IEnumerable<ManagerViewDto>? GetAllManager()
        {
            IEnumerable<ManagerViewDto>? list = null;


            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    string Query = @" Select * from GetAll_Managers();";

                    var Result = connection.Query<ManagerViewDto>(Query,commandType: CommandType.Text);

                    if(Result != null)
                    {
                        // return deferred execution
                        list = Result;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

            return list;
        }

    }
}
