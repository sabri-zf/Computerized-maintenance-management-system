using Computerized_maintenance_Logic_layer.Module.AssetsManagement;
using Computerized_maintenance_Logic_layer.Module.User_Management;
using computrized_maintenance_Data_Access.Data;
using computrized_maintenance_Data_Access.DTO;
using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using computrized_maintenance_Data_Access.Enumes;
using computrized_maintenance_Data_Access.Misc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Test_Data_Of_CMMS
{

    static class AdminProecces
    {
        //public static void AddAdmin(PersonDto personDto, Userdto_DataAccess userdto)
        //{
        //    ClsUsers user = new ClsUsers(personDto, userdto);

        //    if (user.Save())
        //    {
        //        ClsAdmins AdminInstance = new ClsAdmins();
        //        AdminInstance.DTO = new AdminDto { AdminID = -1, UserID = user.UserID };

        //        if (AdminInstance.Save())
        //        {
        //            Console.WriteLine("Save Admin Has been Successed :)");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Save Admin Has Been failed");
        //        }
        //    }

        //}
        //public static void UpdateAdmin(int AdminID, Userdto_DataAccess userDto)
        //{
        //    if (ClsAdmins.IsExistAdmin(AdminID))
        //    {
        //        ClsAdmins? Admin = ClsAdmins.Find(AdminID);
        //        if (Admin == null)
        //        {
        //            Console.WriteLine("Admin Is Not Found");
        //            return;
        //        }
        //        ClsUsers? user = ClsUsers.FindUser(Admin.UserID);
        //        if (user == null)
        //        {
        //            Console.WriteLine("Admin Is Not Found");
        //            return;
        //        }

        //        userDto.UserID = user.UserID;
        //        user.dto = userDto;

        //        if (user.Save())
        //        {
        //            Console.WriteLine("Update Admin Is Successfully");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Update Admin is Fail");
        //        }
        //        return;
        //    }


        //    Console.WriteLine($"Admin Has Id #{AdminID} is Exist");

        //}
        //public static void GetAllAdmins()
        //{
        //    var List = ClsAdmins.GetAllAdmin();

        //    if (List != null)
        //    {
        //        foreach (var Item in List)
        //        {
        //            Console.WriteLine(Item);
        //        }

        //        return;
        //    }

        //    Console.WriteLine("List Is Empty ,Not Data Found");
        //}
        //public static void DeleteAdmin(int AdminID)
        //{
        //    if (AdminID < 1)
        //    {
        //        Console.WriteLine($"Operation can't completed AdminID ({AdminID}) outside valid");
        //    }

        //    if (ClsAdmins.DeleteAdmin(AdminID))
        //    {
        //        Console.WriteLine("Delete Operation Has been Successed");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Delete operation Has been Failed");
        //    }
        //}

        ////test CRUD opteraion Of Manager 

        //public static void AddManager(PersonDto personDto, Userdto_DataAccess userdto)
        //{
        //    ClsUsers user = new ClsUsers(personDto, userdto);

        //    if (user.Save())
        //    {
        //        ManagerDto managerDto = new ManagerDto()
        //        {
        //            UserID = user.UserID,
        //            CreatedByAdmin = 2,
        //            DepartmentID = 4,
        //            ManagedBy = null,
        //        };

        //        ClsManagers manager = new ClsManagers(managerDto);
        //        //AdminInstance.DTO = new AdminDto { AdminID = -1, UserID = user.UserID };

        //        if (manager.Save())
        //        {
        //            Console.WriteLine("Save Admin Has been Successed :)");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Save Admin Has Been failed");
        //        }
        //    }

        //}

        //public static void UpdateManager(ManagerDto managerDto, Userdto_DataAccess userDto)
        //{
        //    if (ClsManagers.IsExist(managerDto.ManagerID))
        //    {
        //        ClsManagers? manager = ClsManagers.Find(managerDto.ManagerID);

        //        if (manager == null)
        //        {
        //            Console.WriteLine($"Manager has ID {managerDto.ManagerID} Is Not Found");
        //            return;
        //        }

        //        manager.DepartmentID = managerDto.DepartmentID;
        //        if (managerDto.ManagedBy.HasValue || managerDto.ManagedBy > 0)
        //        {
        //            manager.ManagedBy = managerDto.ManagedBy;
        //        }

        //        if (manager.Save())
        //        {
        //            ClsUsers? user = ClsUsers.FindUser(manager.UserID);
        //            if (user == null)
        //            {
        //                Console.WriteLine("Manager Is Not Found");
        //                return;
        //            }

        //            userDto.UserID = user.UserID;
        //            user.dto = userDto;

        //            if (user.Save())
        //            {
        //                Console.WriteLine("Update Manager is Successfully");
        //            }
        //            else
        //            {
        //                Console.WriteLine("Upadete Manager Is Fail");
        //            }
        //            //return;
        //        }
        //        else
        //        {
        //            Console.WriteLine("Update Manager Is Fail... :(");
        //        }

        //        return;
        //    }

        //    Console.WriteLine($"Manager of ID {managerDto.ManagerID} Is not Exit");
        //}

        //public static void DeleteManager(int managerID)
        //{
           
             
        //        if (ClsManagers.IsExist(managerID))
        //        {
        //        ClsManagers? manager = ClsManagers.Find(managerID);
        //        if(manager == null) return;

        //            if (ClsManagers.DeleteManager(manager.ManagerDto))
        //            {
        //                Console.WriteLine("Delete Manager Successfull");

        //                return;
        //            }
        //        }

        //        Console.WriteLine("Delete Manager Fail .. :)");
            
        //}

        //public static void GetAllManager()
        //{

        //    var lists =ClsManagers.GetAllManager();

        //    if (lists != null)
        //    {

        //        foreach (var item in lists)
        //        {
        //            Console.WriteLine(item);
        //        }
        //        return;
        //    }

        //    Console.WriteLine("Data Not Found");
        //}
        internal class Startup
        {


            static void Main(string[] args)
            {

               
              

                Console.ReadKey();

            }
        }
    }
}
