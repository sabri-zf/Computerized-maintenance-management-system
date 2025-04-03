using computrized_maintenance_Data_Access.DTO;
using computrized_maintenance_Data_Access.Misc;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace computrized_maintenance_Data_Access
{
    public class DataAccessAdmin
    {


        public static bool FindByID(int ID,ref AdminDto admin)
        {
            if(ID < 1) return false;


            bool IsFound = false;
            using (IDbConnection connection = new SqlConnection(connectionString: ClsUtility.ConnectionString))
            {
                try
                {

                    DynamicParameters AdminIDparam = new DynamicParameters();
                    AdminIDparam.Add("@AdminID", ID);

                    connection.Open();
                    var Result = connection.Query<AdminDto>("Sp_FindByAdminID",AdminIDparam,commandType: CommandType.StoredProcedure).SingleOrDefault();

                    if(Result is null)
                    {
                        IsFound = false;
                    }else
                    {
                        admin = Result;
                        IsFound = true;
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }

            return IsFound;
        }

        public static int AddNewAdmin(AdminDto admin)
        {
            if (admin == null) return -1;

            int PersonId = -1;

            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters AdminParam = new DynamicParameters();
                    AdminParam.Add("@UserID", admin.UserID);
                    AdminParam.Add("@AdminID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    connection.Open();

                    connection.Execute("Sp_AddnewAdmin", param: AdminParam, commandType: CommandType.StoredProcedure);

                    PersonId = AdminParam.Get<int>("@AdminID");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            return PersonId;
        }

        public static bool DeleteAdmin(int? ID)
        {
            if(ID < 1) return false;

            bool IsDeleted = false;
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {
                    DynamicParameters AdminParam = new DynamicParameters();
                    AdminParam.Add("@AdminID", ID);

                    connection.Open();

                    IsDeleted = connection.Execute("Sp_DeleteAdmin", AdminParam, commandType: CommandType.StoredProcedure) > 0;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    IsDeleted = false;
                }
            }

            return IsDeleted;
        }

        public static bool IsExistAdmin(int ID)
        {
            if (ID < 1) return false;

            bool IsExist = false;

            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {

                try
                {
                    DynamicParameters AdminParam = new DynamicParameters();
                    AdminParam.Add("@AdminID", ID);

                    connection.Open();
                    string querey = @"select Found = 1 from Admins where AdminID = @AdminID";

                    var Result = connection.ExecuteScalar(querey, AdminParam, commandType: CommandType.Text);

                    if (Result != null && (int)Result > 0)
                    {
                        IsExist = true;
                    }
                    else
                    {
                        IsExist = false;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    IsExist = false;
                }
            }
            return IsExist;
        }

        public static List<AdminViewDto>? GetAllAdmins()
        {
            using (IDbConnection connection = new SqlConnection(ClsUtility.ConnectionString))
            {
                try
                {

                    string querey = @"select * From GetAll_Adimns()";

                    connection.Open();
                    var Result =  connection.Query<AdminViewDto>(querey,null,commandType: CommandType.Text).ToList();

                    if(Result != null)
                    {
                        connection.Close();
                        connection.Dispose();
                        return Result;
                    }

                }
                catch
                (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return null;
        }
    }
}
