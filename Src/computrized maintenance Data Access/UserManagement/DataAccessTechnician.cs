using computrized_maintenance_Data_Access.DTO;
using computrized_maintenance_Data_Access.Misc;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace computrized_maintenance_Data_Access.UserManagement
{
    public class DataAccessTechnician
    {

        public static bool Find(int? ID, ref TechnicianTableDto dto)
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
                    var Result = connection.Query<TechnicianTableDto>("Sp_FindByTechnicianID", TechnicianParam, commandType: CommandType.StoredProcedure).SingleOrDefault();

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


        public static int AddNewTechnician(TechnicianTableDto dto)
        {
            if(dto == null) return -1;

            int TechID = -1;
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

        public static bool UpdateTechnician(TechnicianTableDto dto)
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

        public static bool DeleteTechnician(int ID)
        {
            if(ID< 1) return false;

            bool HasBeenDeleted = false;
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {

                    var TechParam = new DynamicParameters();
                    TechParam.Add("@TechnicianID", ID);

                    connection.Open();
                    HasBeenDeleted = connection.Execute("Sp_DeleteThechnician", TechParam, commandType: CommandType.StoredProcedure) > 0;

                }
                catch (Exception ex)
                {
                    throw;
                }

                return HasBeenDeleted;
            }
        }


        public static List<TechnicianTableViewDto>? GetAll()
        {

            IEnumerable<TechnicianTableViewDto>? TechList= new List<TechnicianTableViewDto>();

            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {

                try
                {

                    var Result = connection.Query<TechnicianTableViewDto>("Sp_GetAllTechnicians", commandType: CommandType.StoredProcedure);

                    if (Result != null)
                    {
                        TechList = Result;
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return TechList.ToList();
        }
    }
}
