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
        private CRUDmode.Mode_Save _mode;
        public int? AdminID { get; private set; }
        public  int? UserID { get;  set; }
        public ClsUsers? Users { get; set; }

        public AdminDto? DTO { get; set; }
        public ClsAdmins() 
        {
            this.AdminID = null;
            this.UserID = null;
            this.DTO = null;

            _mode = CRUDmode.Mode_Save.AddNew;
        }

        private ClsAdmins(int? AdminID , int ? UserID, CRUDmode.Mode_Save Mode = CRUDmode.Mode_Save.AddNew)
        {
            this.AdminID = AdminID;
            this.UserID = UserID;
            this.Users = null;
            this._mode = Mode;
        }

        public static ClsAdmins? Find(int ID)
        {
            var Dto = new AdminDto();
            if(DataAccessAdmin.FindByID(ID, Dto))
            {
                new ClsAdmins(ID, Dto.UserID, CRUDmode.Mode_Save.Update);
            }

            return null;
        }

        private bool AddnewAdmin()
        {
            this.AdminID = DataAccessAdmin.AddNewAdmin(this.DTO);

            return (this.AdminID.HasValue && this.AdminID > 0);
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
                case CRUDmode.Mode_Save.AddNew:
                    if (AddnewAdmin())
                    {
                        this._mode = CRUDmode.Mode_Save.Update;
                        return true;
                    }
                 return false;
            }

            return false;
        }

        public List<AdminViewDto>? GetAllAdmin()
        {
            return DataAccessAdmin.GetAllAdmins();
        }


    }
}
