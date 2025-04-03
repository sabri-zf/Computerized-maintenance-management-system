using Computerized_maintenance_Logic_layer.Module.User_Management.Enums;
using computrized_maintenance_Data_Access;
using computrized_maintenance_Data_Access.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computerized_maintenance_Logic_layer.Module.User_Management
{
    public class ClsAdmins
    {
        private Mode_Save _mode;
        public int AdminID { get; private set; }
        public  int? UserID { get;  set; }
        public ClsUsers? Users { get; set; }

        public AdminDto? DTO { get; set; }
        public ClsAdmins() 
        {
            this.AdminID = -1;
            this.UserID = null;
            this.DTO = null;

            _mode = Mode_Save.AddNew;
        }

        private ClsAdmins(int adminID , int ? userID)
        {
            this.AdminID = adminID;
            this.UserID = userID;
            this.Users = ClsUsers.FindUser(UserID);
            this._mode = Mode_Save.Update;
        }

        public static ClsAdmins? Find(int ID)
        {
            var Dto = new AdminDto();
            if(DataAccessAdmin.FindByID(ID, ref Dto))
            {
               return new ClsAdmins(ID, Dto.UserID);
            }

            return null;
        }

        private bool AddnewAdmin()
        {
            this.AdminID = DataAccessAdmin.AddNewAdmin(this.DTO);

            return (this.AdminID > 0);
        }

        public static bool DeleteAdmin(int? ID)
        {
            return DataAccessAdmin.DeleteAdmin(ID);
        }

        public bool DeleteAdmin()
        {
            return DeleteAdmin(this.AdminID);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case Mode_Save.AddNew:
                    if (AddnewAdmin())
                    {
                        this._mode = Mode_Save.Update;
                        return true;
                    }
                 return false;
            }

            return false;
        }

        public static bool IsExistAdmin(int AdminID)
        {
            return DataAccessAdmin.IsExistAdmin(AdminID);
        }

        public bool IsExistAdmin()
        {
            return IsExistAdmin(this.AdminID);
        }

        public static List<AdminViewDto>? GetAllAdmin()
        {
            return DataAccessAdmin.GetAllAdmins();
        }

    }
}
