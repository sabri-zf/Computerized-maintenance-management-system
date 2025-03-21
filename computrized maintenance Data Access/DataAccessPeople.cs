
using computrized_maintenance_Data_Access.DTO;
using computrized_maintenance_Data_Access.Misc;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
namespace computrized_maintenance_Data_Access
{
    public class DataAccessPeople
    {
       
        // using Deapper to excuted my database result set 

        public static async Task<List<PersonDto>> GetPeopleAsync()
        {
            //List<PersonDto>? people = new List<PersonDto>();

            IEnumerable<PersonDto> people;

            using (SqlConnection connection = new SqlConnection(connectionString: ClsUtility.ConnectionString))
            {
                try
                {
                    string ExecuteStoreProcedure = "Sp_GetAllPeople";

                    people = await connection.QueryAsync<PersonDto>(ExecuteStoreProcedure, commandType: System.Data.CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }

            }
            return people.ToList();
        }

        public static List<PersonDto> GetPeople()
        {
            IEnumerable<PersonDto> people;

            using (SqlConnection connection = new SqlConnection(connectionString: ClsUtility.ConnectionString))
            {
                try
                {
                    string ExecuteStoreProcedure = "Sp_GetAllPeople";

                    people = connection.Query<PersonDto>(ExecuteStoreProcedure, commandType: System.Data.CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }

            }
            return people.ToList();
        }


        public static int? AddNewPerson(PersonDto person)
        {
            if (person == null) return null;

            int? PersonID = null;

            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@FirstName", person.FirstName);
            parameter.Add("@LastName", person.LastName);
            parameter.Add("@Email", person.Email);
            parameter.Add("@Phone", person.Phone);
            parameter.Add("@BirthDay", person.BirthDay);
            parameter.Add("@Addrees", person.Addrees);
            parameter.Add("@PersonID", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            using (SqlConnection connection = new SqlConnection(connectionString: ClsUtility.ConnectionString))
            {
                try
                {
                    connection.Open();
                    connection.Execute(sql: "Sp_AddNewPeople", parameter, commandType: System.Data.CommandType.StoredProcedure);

                    PersonID = parameter.Get<int>("@PersonID");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }


            }
            return PersonID;
        }


        public static bool UpdatePerson(PersonDto person)
        {
            if (person == null) return false;

            bool IsUpdateSuccessed = false;

            using (SqlConnection connection = new SqlConnection(connectionString: ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@FirstName", person.FirstName);
                    parameter.Add("@LastName", person.LastName);
                    parameter.Add("@Email", person.Email);
                    parameter.Add("@Phone", person.Phone);
                    parameter.Add("@BirthDay", person.BirthDay);
                    parameter.Add("@Addrees", person.Addrees);
                    parameter.Add("@PersonID", person.PersonID);


                    connection.Open();

                    IsUpdateSuccessed = connection.Execute("Sp_UpdatePeople", parameter, commandType: System.Data.CommandType.StoredProcedure) > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    IsUpdateSuccessed = false;
                }
            }
            return IsUpdateSuccessed;
        }


        public static bool DeletePerson(int? ID)
        {
            if (ID < 1) return false;

            bool IsDelelteSuccessed = false;

            using (SqlConnection connection = new SqlConnection(connectionString: ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters DeleteParameter = new DynamicParameters();
                    DeleteParameter.Add("@PersonID" ,ID);

                    connection.Open();

                    IsDelelteSuccessed = connection.Execute("Sp_DeletePeople", DeleteParameter, commandType: CommandType.StoredProcedure) > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    IsDelelteSuccessed = false;
                    throw;
                }
            }
            return IsDelelteSuccessed;
        }


        public static bool IsExistPerson(int? ID)
        {
            if(ID < 1) return false;

            bool IsExist = false;

            using (SqlConnection connection = new SqlConnection(connectionString: ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters IsExistParameter = new DynamicParameters();
                    IsExistParameter.Add("@PersonID", ID);

                    string querey = @"select  Found = 1 from People where PersonID = @PersonID";

                    IsExist = connection.ExecuteScalar<int>(querey,IsExistParameter, commandType: CommandType.Text) > 0;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    IsExist = false;
                    throw;
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    IsExist = false;
                    throw;
                }
            }
            return IsExist;
        }
    }
}
