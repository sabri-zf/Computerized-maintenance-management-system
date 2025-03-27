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
    public  class DataAccessCountry
    {


        public static bool Find(int? ID, ref CountryDto Dto)
        {
            if (ID == null || ID < 1) return false;

            bool IsFound = false;
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters DepartmentParam = new DynamicParameters();
                    DepartmentParam.Add("@ID", ID);

                    string Query = "select * from Countries where CountryID = @ID";
                    connection.Open();
                    var Result = connection.Query<CountryDto>(Query, DepartmentParam, commandType: CommandType.Text).SingleOrDefault();

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

        public static string? GetDepartmentName(int? ID)
        {
            if (ID == null || ID < 1) return null;

            string? Name = null;
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters Departmentparam = new DynamicParameters();
                    Departmentparam.Add("@ID", ID);

                    string query = @"select top 1 CountryName from Countries where CountryID = @ID";

                    connection.Open();
                    Name = connection.Query<string>(query, Departmentparam, commandType: CommandType.Text).SingleOrDefault();

                }
                catch (Exception ex)
                {
                    throw;
                }

            }

            return Name;

        }

        public static IEnumerable<CountryDto>? GetAll()
        {

            IEnumerable<CountryDto>? CountryList = null;

            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    string Query = @"Select CountryID ,CountryName from Countries";

                    connection.Open();

                    var Result = connection.Query<CountryDto>(Query);

                    if (Result != null)
                    {
                        CountryList = Result;
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }

                return CountryList;
            }
        }

    }
}
