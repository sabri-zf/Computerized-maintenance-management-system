using computrized_maintenance_Data_Access.DTO;
using computrized_maintenance_Data_Access.Misc;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
namespace computrized_maintenance_Data_Access.UserManagement
{
    public sealed class PeopleRepo
    {

        // using Deapper to excuted my database result set 

        public async Task<PersonDtoResponse?> FindAsync(int PersonID)
        {
            if (PersonID < 1) return null;


            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters PersonParam = new DynamicParameters();
                    PersonParam.Add("@PersonID", PersonID);

                    string query = @"Select * From People where PersonID = @PersonID";

                    connection.Open();
                    var result = await connection.QueryFirstAsync<PersonDtoResponse>(query, PersonParam, commandType: CommandType.Text);

                    if (result is not null) return result;

                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }

                return null;
            }
        }
        public async Task<IEnumerable<PersonDtoResponse>?> GetPeopleAsync()
        {

            using (SqlConnection connection = new SqlConnection(connectionString: ClsUtility.ConnectionString))
            {
                try
                {

                    string ExecuteStoreProcedure = "Sp_GetAllPeople";

                    connection.Open();
                    var peopleList = await connection.QueryAsync<PersonDtoResponse>(ExecuteStoreProcedure, commandType: CommandType.StoredProcedure);
                    connection.Close();

                    if (peopleList is not null) return peopleList;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }

            }
            return null;
        }

        public async Task<bool> AddNewPerson(PersonDtoResponse? person)
        {
            if (person == null) return false;
            if (string.IsNullOrEmpty(person.FirstName)) return false;
            if (string.IsNullOrEmpty(person.LastName)) return false;
            if (string.IsNullOrEmpty(person.Phone)) return false;
            if (string.IsNullOrEmpty(person.Email)) return false;
            if (string.IsNullOrEmpty(person.Addrees)) return false;



            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@FirstName", person.FirstName);
            parameter.Add("@LastName", person.LastName);
            parameter.Add("@Email", person.Email);
            parameter.Add("@Phone", person.Phone);
            parameter.Add("@BirthDay", person.BirthDay);
            parameter.Add("@Addrees", person.Addrees);
            parameter.Add("@PersonID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (SqlConnection connection = new SqlConnection(connectionString: ClsUtility.ConnectionString))
            {
                try
                {
                    connection.Open();
                    var IsAdded = await connection.ExecuteAsync(sql: "Sp_AddNewPeople", parameter, commandType: CommandType.StoredProcedure) > 0;
                    connection.Close();

                    if (IsAdded)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }


            }
            return false;
        }

        public async Task<bool> UpdatePerson(PersonDtoRequest? person)
        {
            if (person == null) return false;
            if (person.PersonId < 1) return false;
            if (string.IsNullOrEmpty(person.FirstName)) return false;
            if (string.IsNullOrEmpty(person.LastName)) return false;
            if (string.IsNullOrEmpty(person.Phone)) return false;
            if (string.IsNullOrEmpty(person.Email)) return false;
            if (string.IsNullOrEmpty(person.Addrees)) return false;

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
                    parameter.Add("@PersonID", person.PersonId);


                    connection.Open();
                    var IsUpdate = await connection.ExecuteAsync("Sp_UpdatePeople", parameter, commandType: CommandType.StoredProcedure) > 0;
                    connection.Close();

                    if (IsUpdate)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            return false;
        }

        public async Task<bool> DeletePerson(int ID)
        {
            if (ID < 1) return false;

            bool IsDelelteSuccessed = false;

            using (SqlConnection connection = new SqlConnection(connectionString: ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters DeleteParameter = new DynamicParameters();
                    DeleteParameter.Add("@PersonID", ID);

                    connection.Open();
                    IsDelelteSuccessed = await connection.ExecuteAsync("Sp_DeletePeople", DeleteParameter, commandType: CommandType.StoredProcedure) > 0;
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            return IsDelelteSuccessed;
        }

        public async Task<bool> IsExistPerson(int? ID)
        {
            if (ID < 1) return false;

            bool IsExist = false;

            using (SqlConnection connection = new SqlConnection(connectionString: ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters IsExistParameter = new DynamicParameters();
                    IsExistParameter.Add("@PersonID", ID);

                    string querey = @"select  Found = 1 from People where PersonID = @PersonID";

                    IsExist = await connection.ExecuteScalarAsync<int>(querey, IsExistParameter, commandType: CommandType.Text) > 0;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            return IsExist;
        }
    }
}

