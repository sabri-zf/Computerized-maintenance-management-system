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
    public class DataAccesDepartment
    {

        public static string? GetDepartmentName(int? ID)
        {
            if(ID == null || ID < 1) return null;

            string? Name = null;
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters Departmentparam = new DynamicParameters();
                    Departmentparam.Add("@ID",ID);

                    string query = @"select top 1 DepartmentName from Depertments where DepartmentID = @ID";

                    connection.Open();
                    Name = connection.Query<string>(query, Departmentparam,commandType:CommandType.Text).SingleOrDefault();

                }
                catch (Exception ex)
                {
                    throw;
                }
                
            }
            
            return Name;

        }

        public static IEnumerable<DepartmentDto>? GetAll()
        {

            IEnumerable<DepartmentDto>? DepartmentList = null;
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    string Query = @"Select * from Departments";

                    connection.Open();

                    var Result = connection.Query<DepartmentDto>(Query);

                    if (Result != null)
                    {
                        DepartmentList = Result;
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }

                return DepartmentList;
            }
        }

        public static bool Find(int? ID, ref DepartmentDto Dto)
        {
            if (ID == null || ID < 1) return false;

            bool IsFound = false;
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters DepartmentParam = new DynamicParameters();
                    DepartmentParam.Add("@ID", ID);

                    string Query = "select * from Departments where DepartmentID = @ID";
                    connection.Open();
                    var Result = connection.Query<DepartmentDto>(Query, DepartmentParam, commandType: CommandType.Text).SingleOrDefault();

                    if (Result != null)
                    {
                        IsFound = true;
                        Dto = Result;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return IsFound;
        }
    }
}
