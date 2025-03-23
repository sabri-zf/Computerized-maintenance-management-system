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


    }
}
