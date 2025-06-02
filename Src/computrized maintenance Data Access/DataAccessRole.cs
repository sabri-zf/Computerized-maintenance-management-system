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
    public class DataAccessRole
    {


        public static bool Find(int? id, ref RoleTableDto dto)
        {

            if (id < 1 || id is null) return false;

            bool IsFound = false;
            using (IDbConnection Connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {

                    DynamicParameters RoleParam = new DynamicParameters();
                    RoleParam.Add("@RoleID", id);


                    Connection.Open();
                    RoleTableDto? Result = Connection.Query<RoleTableDto>("Sp_GetRoleById",RoleParam,commandType: CommandType.StoredProcedure).SingleOrDefault();

                    if (Result != null)
                    {
                        IsFound = true;
                        dto = Result;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);

                    throw;
                }
            }
            return IsFound;
        }

        public static string? RoleName(int? id)
        {
            if (id < 1 || id is null) return null;

            string? RoleName = null;
            using (IDbConnection Connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters RoleParam = new DynamicParameters();
                    RoleParam.Add("@RoleID", id);


                    string? Result = Connection.Query<string>("Sp_RoleName",RoleParam,commandType:CommandType.StoredProcedure).SingleOrDefault();

                    if (Result != null)
                    {
                        RoleName = Result;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }

                return RoleName;
            }
        }

        public static List<RoleTableDto> GetAllRoles()
        {
            List<RoleTableDto> List = new List<RoleTableDto>();

            using (IDbConnection Connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {

                    var Result = Connection.Query<RoleTableDto>("Sp_GetAllRoles",commandType:CommandType.StoredProcedure);

                    if (Result != null)
                    {
                        List = Result.ToList();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
                return List;
        }
    }
}
