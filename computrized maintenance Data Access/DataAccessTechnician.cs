using computrized_maintenance_Data_Access.DTO;
using computrized_maintenance_Data_Access.Misc;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computrized_maintenance_Data_Access
{
    public class DataAccessTechnician
    {

        public static bool Find(int? ID, ref TechnicianDto dto)
        {

            if (ID is null || ID < 1) return false;

            bool IsFound = false;
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {

                    DynamicParameters TechnicianParam = new DynamicParameters();
                    TechnicianParam.Add("@TechnincianID", ID);

                    connection.Open();
                    var Result = connection.Query<TechnicianDto>("Sp_FindByTechnicianID", TechnicianParam, commandType: CommandType.StoredProcedure).SingleOrDefault();

                    if (Result != null)
                    {
                        IsFound = true;
                        dto = Result;
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }

            }

            return IsFound;
        }


        public static int? AddNewTechnician(TechnicianDto dto)
        {
            if(dto == null) return null;

            int? TechID = null;
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters TechParam = new DynamicParameters();
                    TechParam.Add("@UserID",dto.UserID);
                    TechParam.Add("@DepartmentID",dto.DepartmentID);
                    TechParam.Add("@ManagedBy",dto.ManagedBy);
                    TechParam.Add("@CreatedBy",dto.CreatedByAdmin);
                    TechParam.Add("@TechnicianID",dbType:DbType.Int32,direction:ParameterDirection.Output);

                    connection.Open();

                    connection.Execute("Sp_AddNewTechnician",TechParam, commandType: CommandType.StoredProcedure);

                    TechID = TechParam.Get<int>("@TechnicianID");

                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return TechID;
        }

        public static bool UpdateTechnician(TechnicianDto dto)
        {
            if (dto == null ) return false;

            bool IsUpdateted = false;
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters TechParam = new DynamicParameters();
                    TechParam.Add("@TechnicianID", dto.TechnicianID);
                    TechParam.Add("@DepartmentID", dto.DepartmentID);
                    TechParam.Add("@ManagedBy", dto.ManagedBy);

                    IsUpdateted = connection.Execute("Sp_UpdateTechnician", TechParam, commandType: CommandType.StoredProcedure) > 0;


                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return IsUpdateted;
        }
    }
}
